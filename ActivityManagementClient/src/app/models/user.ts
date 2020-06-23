export interface User {

    id:number,
    userName:string,
    firstName:string,
    lastName:string,
    phoneNumber:number,
    email:string,
    imageUrl:string,
    birthDate:string,
    gender:gender,
    role:string,
}

enum gender{
    men=1,
    women=2
}