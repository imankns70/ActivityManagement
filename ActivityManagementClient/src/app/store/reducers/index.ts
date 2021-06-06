import { ActionReducerMap, createFeatureSelector, createSelector } from "@ngrx/store";
import { loggedUserReducer, UserLoggedState } from "./loggedUser.reducer";
import * as fromRouter from '@ngrx/router-store';
import { RouterStateUrl } from "../_model/routerStateUrl";


 
export interface State {
    routerReducer: fromRouter.RouterReducerState<RouterStateUrl>;
    loggedUser: UserLoggedState;
}

export const reducers: ActionReducerMap<State> = {
    routerReducer: fromRouter.routerReducer,
    loggedUser: loggedUserReducer
}
 