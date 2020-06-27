export interface ApiResult{
    isSuccess:boolean,
    statusCode: statusCode,
    message:string[],
    data:object

}
enum statusCode{
        success = 0,

        serverError = 1,

        badRequest = 2,

        notFound = 3,

        listEmpty = 4,

        logicError = 5,

        unAuthorized = 6
}