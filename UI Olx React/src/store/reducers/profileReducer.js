import {UPDATE_PROFILE, PROFILE_ERROR, UNAUTHORIZED, GET_PROFILE} from '../types'

const initialUser = {
    accessToken: '',
    userId: '',
    userName: '',
    email: '',
    imageUrl: '',
    roles: [],
    isLogin: false
}
const initialState = {
    profileData:{
        id: '',
        userName: '',
        email: '',
        imagePath: '',
    }
}

export default function(state = initialState, action){
    debugger
    switch(action.type){

        case UPDATE_PROFILE:
            debugger
            const retrievedStoreStr = localStorage.getItem('userData') 
            const userData = JSON.parse(retrievedStoreStr) 
            userData.userName = action.payload.userName
            userData.imageUrl = action.payload.imagePath
            userData.email = action.payload.email
            localStorage.setItem("userData", JSON.stringify(userData));
        return {
            ...state,
        }
        case GET_PROFILE:
        return {
            ...state,
            profileData: action.payload
        }
        case PROFILE_ERROR:
            return {
                ...state,
            }
        case UNAUTHORIZED:
            localStorage.removeItem("userData");
            localStorage.setItem("userData", JSON.stringify(initialUser)); 
            return {
                ...state,
                profileData: initialState.profileData
            }
        default: return state
    }
}