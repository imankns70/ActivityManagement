import { StatusCode } from './enums/StatusCode';

export interface ApiResult{
    isSuccess:boolean,
    statusCode: StatusCode,
    message:string[],
    data:object

}
 