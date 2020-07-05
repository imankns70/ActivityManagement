import { gender } from './enums/gender';

export interface User {

    id: number,
    userName: string,
    firstName: string,
    lastName: string,
    phoneNumber: number,
    email: string,
    Image: string,
    PersianBirthDate: string,
    gender?: gender,
    //role: string,
}







 

 