import { REGISTER, REGISTER_ERROR } from '../types'

const initialState = {
    message: ''
}

export default function (state = initialState, action) {
    switch (action.type) {
        case REGISTER:
            return{
                ...state,
                message: action.payload
            }
        case REGISTER_ERROR:

            console.log("Something went wrong");
            return state;
        default: return state
    }
}