import { gender } from './enums/gender';

export interface User {

    id: number;
    userName: string;
    firstName: string;
    lastName: string;
    phoneNumber: number;
    email: string;
    image: string;
    persianBirthDate: string;
    gender?: gender;
    //role: string,
}







 

 