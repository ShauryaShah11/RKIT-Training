class PersonalInfo {
    constructor(
        fullName,
        userName,
        email,
        phoneNumber,
        password,
    ) {
        this.fullName = fullName;
        this.userName = userName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.password = password;
    }

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

class BusinessInfo {
    constructor(businessName, businessType) {
        this.businessName = businessName;
        this.businessType = businessType;
    }

    static create(businessName, businessType){
        return new BusinessInfo(businessName, businessType);
    }
}

class FinancialDetails {
    constructor(currency, accountingMethod, startDate) {
        this.currency = currency;
        this.accountingMethod = accountingMethod;
        this.startDate = startDate;
    }
    static create(currency, accountingMethod, startDate){
        return new FinancialDetails(currency, accountingMethod, startDate);
    }
}

class ContactDetails {
    constructor(address, city, businessCertificate) {
        this.address = address;
        this.city = city;
        this.businessCertificate = businessCertificate;
    }
    static create(address, city, businessCertificate){
        return new ContactDetails(address, city, businessCertificate);
    }
}

class User {
    constructor(personalInfo, businessInfo, financialDetails, contactDetails) {
        this.personalInfo = personalInfo;
        this.businessInfo = businessInfo;
        this.financialDetails = financialDetails;
        this.contactDetails = contactDetails;
    }
}
