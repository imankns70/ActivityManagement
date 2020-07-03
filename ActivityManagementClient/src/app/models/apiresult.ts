import { StatusCode } from './enums/StatusCode';

export class ApiResult {
    constructor() {
        this.message = new Array<string>();
        this.isSuccess= false;
        this.statusCode=0;
        this.data= new Object;
    }
    isSuccess: boolean;
    statusCode: StatusCode;
    message: string[];
    data: object;

}
