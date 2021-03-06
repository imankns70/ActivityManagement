import { Action } from '@ngrx/store'
import { User } from 'src/app/models/user/user';

export enum loggedIUserTypes {

    LOADLOGGEDUSER = '[Logged User] Load',
    LOADLOGGEDUSER_SUCCESS = '[Logged User] Success',
    LOADLOGGEDUSER_FAIL = '[Logged User] Fail',
    EDIT_LOGGEDUSER = '[Logged User] Edit',
    RESET_LOGGEDUSER = '[Logged User] Reset',
    EDIT_LOGGEDUSERPHOTOURL = '[Logged User PhotoUrl] Edit',
    EDIT_LOGGEDUSERFIRSTNAME = '[Logged User FirstName] Edit',

}

export class LoadLoggedUser implements Action {
    readonly type = loggedIUserTypes.LOADLOGGEDUSER;


}

export class LoadLoggedUserSuccess implements Action {
    readonly type = loggedIUserTypes.LOADLOGGEDUSER_SUCCESS;
    constructor(public payload: User) { }

}

export class LoadLoggedUserFail implements Action {
    readonly type = loggedIUserTypes.LOADLOGGEDUSER_FAIL;
    constructor(public payload: string) { }

}
export class EditLoggedUser implements Action {
    readonly type = loggedIUserTypes.EDIT_LOGGEDUSER;
    constructor(public payload: User) { }

}

export class ResetLoggedUser implements Action {
    readonly type = loggedIUserTypes.RESET_LOGGEDUSER;


}

export class EditLoggedUserPhotoUrl implements Action {
    readonly type = loggedIUserTypes.EDIT_LOGGEDUSERPHOTOURL;
    constructor(public payload: string) { }

}

export class EditLoggedUserName implements Action {
    readonly type = loggedIUserTypes.EDIT_LOGGEDUSERFIRSTNAME;
    constructor(public payload: string) { }

}



export type All =
    EditLoggedUser |
    ResetLoggedUser |
    EditLoggedUserPhotoUrl |
    EditLoggedUserName |
    LoadLoggedUser |
    LoadLoggedUserSuccess |
    LoadLoggedUserFail
