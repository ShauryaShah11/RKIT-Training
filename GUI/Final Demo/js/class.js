// Class to store personal information
class PersonalInfo {
    constructor(
        fullName, // User's full name
        userName, // User's username
        email, // User's email address
        phoneNumber, // User's phone number
        password // User's password
    ) {
        this.fullName = fullName;
        this.userName = userName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.password = password;
    }

    // Static method to create a new PersonalInfo instance
    static create(fullName, userName, email, phoneNumber, password) {
        return new PersonalInfo(
            fullName,
            userName,
            email,
            phoneNumber,
            password
        );
    }
}

// Class to store business information
class BusinessInfo {
    constructor(businessName, businessType) {
        this.businessName = businessName; // Name of the business
        this.businessType = businessType; // Type of the business
    }

    // Static method to create a new BusinessInfo instance
    static create(businessName, businessType) {
        return new BusinessInfo(businessName, businessType);
    }
}

// Class to store financial details
class FinancialDetails {
    constructor(currency, accountingMethod, startDate) {
        this.currency = currency; // Currency used in the business
        this.accountingMethod = accountingMethod; // Accounting method used
        this.startDate = startDate; // Start date of the business
    }

    // Static method to create a new FinancialDetails instance
    static create(currency, accountingMethod, startDate) {
        return new FinancialDetails(currency, accountingMethod, startDate);
    }
}

// Class to store contact details
class ContactDetails {
    constructor(address, city, businessCertificate) {
        this.address = address; // Business address
        this.city = city; // City where the business is located
        this.businessCertificate = businessCertificate; // Business registration certificate
    }

    // Static method to create a new ContactDetails instance
    static create(address, city, businessCertificate) {
        return new ContactDetails(address, city, businessCertificate);
    }
}

// Class to store all user-related information
class User {
    constructor(personalInfo, businessInfo, financialDetails, contactDetails) {
        this.personalInfo = personalInfo; // Personal information of the user
        this.businessInfo = businessInfo; // Business information of the user
        this.financialDetails = financialDetails; // Financial details of the user
        this.contactDetails = contactDetails; // Contact details of the user
    }
}

// class to store expense data
class Expenses {
    constructor(amount, expenseDate, category) {
        this.amount = amount;
        this.expenseDate = expenseDate;
        this.category = category;
    }

    saveToSession() {
        sessionStorage.setItem("expenseDate", this.expenseDate);
        sessionStorage.setItem("amount", this.amount);
        sessionStorage.setItem("category", this.category);
    }
}