import { gender } from 'src/app/models/enums/gender';

export class User {
    public id: string;
    public firstName: string;
    public lastName: string;
    public userName: string;
    public phoneNumber: number;
    public roleId: number;
    public roleName: string;
    public email: string;
    public image: string;
    public isActive: boolean;
    public phoneNumberConfirmed: boolean;
    public lockoutEnabled: boolean;
    public emailConfirmed: boolean;
    public registerDate: string;
    public persianBirthDate: string;
    public gender: gender;
}





