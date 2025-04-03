$(function () {
    // ============= UTILITY FUNCTIONS =============
    const Utils = {
        isNotEmpty: function(value) {
            return value !== undefined && value !== null && value !== '';
        },
        
        // Clone objects/arrays to avoid reference issues
        clone: function(obj) {
            return JSON.parse(JSON.stringify(obj));
        }
    };

    // ============= DATA PROCESSING MODULES =============
    const DataProcessors = {
        // PAGING
        applyPaging: function(data, skip, take) {
            if (!Array.isArray(data)) return data;
            
            // Handle skip and take parameters
            const startIndex = skip || 0;
            const endIndex = take ? startIndex + take : data.length;
            
            return data.slice(startIndex, endIndex);
        },
        
        // SORTING
        applySorting: function(data, sortInfo) {
            if (!data || !data.length || !sortInfo || !sortInfo.length) {
                return data;
            }
            
            // Clone data to avoid modifying original
            const clonedData = data.slice();
            
            clonedData.sort((a, b) => {
                for (let i = 0; i < sortInfo.length; i++) {
                    const sortItem = sortInfo[i];
                    const field = sortItem.selector;
                    const desc = sortItem.desc;
                    
                    if (a[field] === b[field]) {
                        continue;
                    }
                    
                    let result;
                    // String comparison
                    if (typeof a[field] === 'string' && typeof b[field] === 'string') {
                        result = a[field].localeCompare(b[field]);
                    } else {
                        result = a[field] < b[field] ? -1 : 1;
                    }
                    
                    return desc ? -result : result;
                }
                return 0;
            });
            
            return clonedData;
        },
        
        // GROUPING
        applyGrouping: function(data, groupInfo, groupSummary) {
            if (!data || !data.length || !groupInfo || !groupInfo.length) {
                return data;
            }
            
            const groupField = groupInfo[0].selector;
            const desc = groupInfo[0].desc;
            const groupMap = {};
            
            // Group data by field
            data.forEach(item => {
                const key = item[groupField] !== undefined ? item[groupField] : "(Blank)";
                if (!groupMap[key]) {
                    groupMap[key] = [];
                }
                groupMap[key].push(item);
            });
            
            // Convert to DevExtreme group format
            let result = Object.keys(groupMap).map(key => {
                const items = groupMap[key];
                const group = {
                    key: key,
                    items: items.slice(),
                    count: items.length
                };
                
                // Handle nested groups
                if (groupInfo.length > 1) {
                    const remainingGroups = groupInfo.slice(1);
                    group.items = DataProcessors.applyGrouping(items, remainingGroups, groupSummary);
                }
                
                // Calculate group summaries
                if (groupSummary && groupSummary.length) {
                    group.summary = SummaryCalculator.calculateGroupSummaries(items, groupSummary);
                }
                
                return group;
            });
            
            // Sort groups
            result.sort((a, b) => {
                const result = a.key < b.key ? -1 : (a.key > b.key ? 1 : 0);
                return desc ? -result : result;
            });
            
            return result;
        },
        
        // Count total number of groups (including nested)
        countGroups: function(groupedData) {
            if (!groupedData || !Array.isArray(groupedData)) {
                return 0;
            }
            
            let count = groupedData.length;
            
            groupedData.forEach(group => {
                if (group.items && Array.isArray(group.items) && 
                    group.items.length > 0 && group.items[0] && group.items[0].key !== undefined) {
                    count += DataProcessors.countGroups(group.items);
                }
            });
            
            return count;
        },
        
        // Check if server correctly applied sorting
        checkServerSorting: function(data, sortInfo) {
            if (!data || !data.length || !sortInfo || !sortInfo.length) {
                return true;
            }
            
            const sortField = sortInfo[0].selector;
            const desc = sortInfo[0].desc;
            const checkLimit = Math.min(5, data.length - 1);
            
            for (let i = 0; i < checkLimit; i++) {
                const a = data[i][sortField];
                const b = data[i + 1][sortField];
                
                if (desc) {
                    if (a < b) return false;
                } else {
                    if (a > b) return false;
                }
            }
            
            return true;
        }
    };

    // ============= SUMMARY CALCULATION MODULE =============
    const SummaryCalculator = {
        // Calculate total summaries from data
        calculateTotalSummaries: function(data, summaryItems) {
            if (!summaryItems || !summaryItems.length) return [];
            
            return summaryItems.map(item => {
                const fieldName = item.selector || item.column;
                
                switch (item.summaryType) {
                    case 'count': 
                        return Array.isArray(data) ? data.length : 0;
                    
                    case 'sum': 
                        return this.calculateSum(data, fieldName);
                    
                    case 'min': 
                        return this.calculateMin(data, fieldName);
                    
                    case 'max': 
                        return this.calculateMax(data, fieldName);
                    
                    case 'avg': 
                        return this.calculateAvg(data, fieldName);
                    
                    default: 
                        return 0;
                }
            });
        },
        
        // Calculate group summaries
        calculateGroupSummaries: function(items, groupSummary) {
            if (!groupSummary || !groupSummary.length) return [];
            
            return groupSummary.map(summary => {
                const field = summary.selector || summary.column;
                
                switch (summary.summaryType) {
                    case 'count': 
                        return items.length;
                    
                    case 'sum': 
                        return this.calculateSum(items, field);
                    
                    case 'min': 
                        return this.calculateMin(items, field);
                    
                    case 'max': 
                        return this.calculateMax(items, field);
                    
                    case 'avg': 
                        return this.calculateAvg(items, field);
                    
                    default: 
                        return 0;
                }
            });
        },
        
        // Helper functions for specific calculations
        calculateSum: function(data, field) {
            let sum = 0;
            if (Array.isArray(data)) {
                data.forEach(item => {
                    if (item[field] !== undefined) {
                        const value = Number(item[field]);
                        if (!isNaN(value)) sum += value;
                    }
                });
            }
            return sum;
        },
        
        calculateMin: function(data, field) {
            let min = Infinity;
            if (Array.isArray(data)) {
                data.forEach(item => {
                    if (item[field] !== undefined) {
                        const value = Number(item[field]);
                        if (!isNaN(value) && value < min) min = value;
                    }
                });
            }
            return min === Infinity ? 0 : min;
        },
        
        calculateMax: function(data, field) {
            let max = -Infinity;
            if (Array.isArray(data)) {
                data.forEach(item => {
                    if (item[field] !== undefined) {
                        const value = Number(item[field]);
                        if (!isNaN(value) && value > max) max = value;
                    }
                });
            }
            return max === -Infinity ? 0 : max;
        },
        
        calculateAvg: function(data, field) {
            let sum = 0;
            let count = 0;
            if (Array.isArray(data)) {
                data.forEach(item => {
                    if (item[field] !== undefined) {
                        const value = Number(item[field]);
                        if (!isNaN(value)) {
                            sum += value;
                            count++;
                        }
                    }
                });
            }
            return count > 0 ? sum / count : 0;
        }
    };

    // ============= SERVER COMPATIBILITY MODULE =============
    const ServerCompatibility = {
        // Check if server response needs client processing
        detectClientProcessingNeeds: function(result, loadOptions) {
            return {
                totalCount: loadOptions.requireTotalCount && 
                           (result.totalCount === undefined || result.totalCount === null),
                
                summary: loadOptions.totalSummary && loadOptions.totalSummary.length && 
                        (!result.summary || !result.summary.length),
                
                grouping: loadOptions.group && loadOptions.group.length && 
                         Array.isArray(result) && (!result[0] || result[0].key === undefined),
                
                sorting: loadOptions.sort && loadOptions.sort.length && 
                        Array.isArray(result) && !DataProcessors.checkServerSorting(result, loadOptions.sort),
                
                paging: loadOptions.take !== undefined && 
                       Array.isArray(result) && result.length > loadOptions.take
            };
        },
        
        // Process data based on what server couldn't handle
        processData: function(result, loadOptions, needsClientProcessing) {
            // Clone to avoid modifying original
            let processedData = Utils.clone(result);
            let responseData = {
                data: processedData,
                totalCount: result.totalCount,
                summary: result.summary || [],
                groupCount: result.groupCount
            };
            
            // Handle totalCount
            if (needsClientProcessing.totalCount) {
                responseData.totalCount = Array.isArray(processedData) ? processedData.length : 0;
            }
            
            // Handle summaries
            if (needsClientProcessing.summary && loadOptions.totalSummary) {
                responseData.summary = SummaryCalculator.calculateTotalSummaries(
                    processedData, loadOptions.totalSummary
                );
            }
            
            // Handle sorting (before grouping and paging)
            if (needsClientProcessing.sorting && !needsClientProcessing.grouping && Array.isArray(processedData)) {
                processedData = DataProcessors.applySorting(processedData, loadOptions.sort);
            }
            
            // Handle grouping
            if (needsClientProcessing.grouping) {
                processedData = DataProcessors.applyGrouping(
                    processedData, loadOptions.group, loadOptions.groupSummary
                );
                responseData.groupCount = DataProcessors.countGroups(processedData);
            }
            
            // Handle paging (apply last, after other processing)
            if (needsClientProcessing.paging && !needsClientProcessing.grouping) {
                processedData = DataProcessors.applyPaging(
                    processedData, loadOptions.skip, loadOptions.take
                );
            }
            
            // Update the data property with processed data
            responseData.data = processedData;
            
            return responseData;
        }
    };

    // ============= CUSTOM STORE IMPLEMENTATION =============
    let dataStore = new DevExpress.data.CustomStore({
        key: 'id',
        loadMode: 'processed',
        
        load(loadOptions) {
            const deferred = $.Deferred();
            
            // Prepare parameters for server
            const paramNames = [
                'skip', 'take', 'requireTotalCount', 'requireGroupCount',
                'sort', 'filter', 'totalSummary', 'group', 'groupSummary',
            ];
            
            // Build request arguments
            const args = {};
            args.requireTotalCount = true;
            args.requireGroupCount = loadOptions.group && loadOptions.group.length > 0;
            
            // Add all valid parameters
            paramNames
                .filter(param => Utils.isNotEmpty(loadOptions[param]))
                .forEach(param => { args[param] = JSON.stringify(loadOptions[param]); });
            
            console.log("Load options:", loadOptions);
            
            // Make API request
            $.ajax({
                url: 'https://localhost:44309/api/user',
                dataType: 'json',
                data: args,
                success(result) {
                    // Detect what needs client-side processing
                    const needsClientProcessing = ServerCompatibility.detectClientProcessingNeeds(result, loadOptions);
                    console.log("Client processing needed:", needsClientProcessing);
                    
                    // Process data as needed
                    const processedResponse = ServerCompatibility.processData(result, loadOptions, needsClientProcessing);
                    
                    // Resolve with processed data
                    deferred.resolve(
                        processedResponse.data, 
                        {
                            totalCount: processedResponse.totalCount,
                            summary: processedResponse.summary,
                            groupCount: processedResponse.groupCount
                        }
                    );
                },
                error(e) {
                    console.error('Data loading error:', e);
                    deferred.reject('Data Loading Error');
                },
                timeout: 5000,
            });
            
            return deferred.promise();
        },
    });

    // ============= GRID INITIALIZATION =============
    $('#gridContainer').dxDataGrid({
        dataSource: dataStore,
        keyExpr: "id",
        showBorders: true,
        
        // Remote operations config
        remoteOperations: {
            filtering: true,
            sorting: true,
            paging: true,
            grouping: true,
            summary: true
        },
        
        // Columns
        columns: [
            "id",
            "firstname", 
            "username",
            {
                dataField: "city",
                dataType: "string",
                groupIndex: 0
            },
            {
                dataField: "salary",
                dataType: "number",
                format: "currency"
            }
        ],
        
        // Grouping
        grouping: {
            autoExpandAll: true
        },
        groupPanel: { visible: true },
        
        // Paging
        paging: { pageSize: 10 },
        pager: {
            visible: true,
            showPageSizeSelector: true,
            allowedPageSizes: [5, 10, 20],
            showInfo: true
        },
        
        // Search
        searchPanel: { visible: true },
        filterPanel: { visible: true },
        
        // Summaries
        summary: {
            totalItems: [
                {
                    column: "id",
                    summaryType: "count",
                    displayFormat: "Total: {0}"
                },
                {
                    column: "salary",
                    summaryType: "min",
                    valueFormat: "currency",
                    displayFormat: "Min Salary: {0}"
                }
            ],
            groupItems: [
                {
                    column: "id",
                    summaryType: "count",
                    displayFormat: "Count: {0}",
                    showInGroupFooter: true
                },
                {
                    column: "salary",
                    summaryType: "sum",
                    valueFormat: "currency",
                    displayFormat: "Sum: {0}",
                    alignByColumn: true
                }
            ]
        }
    });
});