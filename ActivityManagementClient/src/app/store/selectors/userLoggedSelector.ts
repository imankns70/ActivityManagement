

import { createFeatureSelector, createSelector } from "@ngrx/store";
import { User } from "src/app/models/user/user";

 
export const selectLoggedUserFirstName = (state: User) => state.firstName;
export const selectLoggedUserPhotoUrl= (state: User) => state.image;
export const selectLoggedUserRoles= (state: User) => state.roles;
 

// return user
export const getUserLoggedState = createFeatureSelector<User>('loggedUser');

 
export const getUserLoggedFirstUser = createSelector(getUserLoggedState, selectLoggedUserFirstName) 
export const getUserLoggedPhotoUrl= createSelector(getUserLoggedState, selectLoggedUserPhotoUrl)
export const getUserLoggedRoles= createSelector(getUserLoggedState, selectLoggedUserRoles)
