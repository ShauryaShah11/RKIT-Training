class PersonalInfo {
    constructor(
        fullName,
        userName,
        email,
        phoneNumber,
        password,
        confirmPassword
    ) {
        this.fullName = fullName;
        this.userName = userName;
        this.email = email;
        this.phoneNumber = phoneNumber;
        this.password = password;
    }
}

class BusinessInfo {
    constructor(businessName, businessType) {
        this.businessName = businessName;
        this.businessType = businessType;
    }
}

class FinancialDetails {
    constructor(currency, accountingMethod, startDate) {
        this.currency = currency;
        this.accountingMethod = accountingMethod;
        this.startDate = startDate;
    }
}

class ContactDetails {
    constructor(address, city, businessCertificate) {
        this.address = address;
        this.city = city;
        this.businessCertificate = businessCertificate;
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
