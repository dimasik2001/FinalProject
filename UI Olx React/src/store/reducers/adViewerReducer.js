import { GET_AD, AD_ERROR, DELETE_USER, DELETE_AD } from '../types'
const initialState = {
    ad: null,
    user: null
}

export default function (state = initialState, action) {
    switch (action.type) {

        case GET_AD:
            debugger

            return {
                ...state,
                ad: action.payload.ad,
                user: action.payload.user
            }
        case DELETE_USER:
            debugger

            return {
                ...state,
                ad: null,
                user: null
            }
        case DELETE_AD:
            debugger

            return {
                ...state,
                ad: null,
            }
        default: return state
    }
}