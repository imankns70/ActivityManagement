import { Action } from "@ngrx/store";

export function helloMessageReducer(state = 'سلام انگولار', action: Action) {

    console.log(action.type, state);

    switch (action.type) {
        case 'PERSIAN':
            return state = 'سلام انگولار';


        case 'ENGLISH':
            return state = 'hello angular';


        default:
            return state;
    }
}