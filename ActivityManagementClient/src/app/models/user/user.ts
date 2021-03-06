import { gender } from 'src/app/models/enums/gender';

export interface User {
    id: number,
    firstName: string,
    lastName: string,
    userName: string,
    password: string,
    phoneNumber: number,
    roleId: number,
    roleName: string,
    roles: Array<string>,
    email: string,
    image: string,
    isActive: boolean,
    phoneNumberConfirmed: boolean
    lockoutEnabled: boolean,
    emailConfirmed: boolean,
    registerDate: string,
    persianBirthDate: string,
    gender: gender,
}





