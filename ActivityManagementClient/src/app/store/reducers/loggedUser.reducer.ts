import { User } from 'src/app/models/user/user';
import * as loggedUserAction from '../actions';
import { EntityState, createEntityAdapter, EntityAdapter } from '@ngrx/entity'
import { gender } from 'src/app/models/enums/gender';


export type Action = loggedUserAction.All;

export const initUserLoggedState: User = {

    id: 0,
    firstName: '',
    lastName: '',
    userName: '',
    password: '',
    phoneNumber: 0,
    roleId: 0,
    roleName: 'string',
    email: '',
    image: '',
    isActive: false,
    phoneNumberConfirmed: false,
    lockoutEnabled: false,
    emailConfirmed: false,
    registerDate: '',
    persianBirthDate: '',
    gender: 1,

}
export function loggedUserReducer(state = initUserLoggedState, action: Action) {
    switch (action.type) {
        case loggedUserAction.loggedIUserTypes.EDIT_LOGGEDUSER:

            return { ...state, user: action.payload }


        case loggedUserAction.loggedIUserTypes.RESET_LOGGEDUSER:

            return initUserLoggedState;

        case loggedUserAction.loggedIUserTypes.EDIT_LOGGEDUSERPHOTOURL:

            return { ...state, photoUrl: action.payload }


        case loggedUserAction.loggedIUserTypes.EDIT_LOGGEDUSERFIRSTNAME:

            return { ...state, firstName: action.payload }



        default:
            return state;
    }
}