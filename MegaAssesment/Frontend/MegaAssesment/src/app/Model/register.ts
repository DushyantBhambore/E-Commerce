export interface IRegisterDto {
    userId: number,
    firstName: string,
    lastName: string,
    email:string,
    username: string,
    password:string,
    mobile: string,
    dob: string,
    roleId: number,
    imageFile: string,
    address: string,
    zipcode: number,
    stateId: number,
    countryId: number,
    isActive: true,
    isDeleted: false
}