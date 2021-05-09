import { TitleCounter } from './titleCounter';
import * as TitleCounterAction from './titleCounter.action';

export type Action = TitleCounterAction.All

const defaultState: TitleCounter = {

    text: 'سلام من موضوع هستم',
    counter: 0,
}

const newState = (state, newDate) => {

    return { ...state, newDate }
    //return Object.assign({},state,newDate);
}

export function titleCounterReducer(state: TitleCounter = defaultState, action: Action) {
    console.log(action.type, state);
    switch (action.type) {
        case TitleCounterAction.EDIT_TITLE:
 
            return newState(state, { text: action.payload })

        case TitleCounterAction.INCREASECounter:

            return newState(state, { counter: state.counter + 1 })

        case TitleCounterAction.DECREASECounter:

            return newState(state, { counter: state.counter - 1 })

        case TitleCounterAction.RESETCounter:
            return defaultStatus;

        default:
            return state;
    }
}