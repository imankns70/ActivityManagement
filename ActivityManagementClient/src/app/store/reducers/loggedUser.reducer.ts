import { User } from 'src/app/models/user/user';
import * as loggedUserAction from '../actions';
// import { EntityState, createEntityAdapter, EntityAdapter } from '@ngrx/entity'
// import { gender } from 'src/app/models/enums/gender';


export type Action = loggedUserAction.All;

export const initUserLoggedState: User = {

    id: 0,
    firstName: '',
    lastName: '',
    userName: '',
    password: '',
    phoneNumber: 0,
    roleId: 0,
    roleName: '',
    roles: [],
    email: '',
    image: '../../../../assets/images/UserPic.png',
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

        case loggedUserAction.loggedIUserTypes.LOADLOGGEDUSER:

            return state;

        case loggedUserAction.loggedIUserTypes.LOADLOGGEDUSER_SUCCESS:
            console.log(action.payload);
            return {
                ...state,
                id: action.payload.id,
                firstName: action.payload.firstName,
                lastName: action.payload.lastName,
                userName: action.payload.userName,
                password: action.payload.password,
                phoneNumber: action.payload.phoneNumber,
                roleId: action.payload.roleId,
                roleName: action.payload.roleName,
                roles: action.payload.roles,
                email: action.payload.email,
                image: action.payload.image,
                isActive: action.payload.isActive,
                phoneNumberConfirmed: action.payload.phoneNumberConfirmed,
                lockoutEnabled: action.payload.lockoutEnabled,
                emailConfirmed: action.payload.emailConfirmed,
                registerDate: action.payload.registerDate,
                persianBirthDate: action.payload.persianBirthDate,
                gender: action.payload.gender,

            }

        case loggedUserAction.loggedIUserTypes.LOADLOGGEDUSER_FAIL:

            return state;
        case loggedUserAction.loggedIUserTypes.EDIT_LOGGEDUSER:

            return {
                ...state,
                id: action.payload.id,
                firstName: action.payload.firstName,
                lastName: action.payload.lastName,
                userName: action.payload.userName,
                password: action.payload.password,
                phoneNumber: action.payload.phoneNumber,
                roleId: action.payload.roleId,
                roleName: action.payload.roleName,
                roles: action.payload.roles,
                email: action.payload.email,
                image: action.payload.image,
                isActive: action.payload.isActive,
                phoneNumberConfirmed: action.payload.phoneNumberConfirmed,
                lockoutEnabled: action.payload.lockoutEnabled,
                emailConfirmed: action.payload.emailConfirmed,
                registerDate: action.payload.registerDate,
                persianBirthDate: action.payload.persianBirthDate,
                gender: action.payload.gender,

                //return Object.assign({}, state, action.payload)
            }


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