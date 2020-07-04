import { gender } from './enums/gender';

export class User {

    id: number;
    userName: string;
    firstName: string;
    lastName: string;
    phoneNumber: number;
    email: string;
    imageUrl: string;
    birthDate: string;
    gender?: gender;
    role: string;
}

