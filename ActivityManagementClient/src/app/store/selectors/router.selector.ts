import { createFeatureSelector, createSelector } from "@ngrx/store";
import { User } from "src/app/models/user/user";
import { RouterStateUrl } from "../_model/routerStateUrl";
import * as fromRouter from '@ngrx/router-store';


export const getRouterParams = 
(state: fromRouter.RouterReducerState<RouterStateUrl>) => state.state.params;
export const getRouterQueryParams = 
(state: fromRouter.RouterReducerState<RouterStateUrl>) => state.state.queryParams;
export const getRouterUrl = 
(state: fromRouter.RouterReducerState<RouterStateUrl>) => state.state.url;
export const getRouterNavigationId = 
(state: fromRouter.RouterReducerState<RouterStateUrl>) => state.navigationId;
 

export const getRouterState = createFeatureSelector
<fromRouter.RouterReducerState<RouterStateUrl>>('routerReducer');

export const getRouterParamsState=createSelector(getRouterState, getRouterParams);

export const getRouterQueryParamsState=createSelector(getRouterState,getRouterQueryParams);

export const getRouterUrlState=createSelector(getRouterState, getRouterUrl);

export const selectLoggedUserName = (state: User) => state.userName;

// return userState
export const getUserLoggedState = createFeatureSelector<User>('loggedUser');

// return userName
export const getUserLoggedUserName = createSelector(getUserLoggedState, selectLoggedUserName)
