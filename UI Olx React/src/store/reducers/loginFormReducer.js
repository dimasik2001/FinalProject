import { LOGIN,LOGIN_ERROR } from '../types'

 const initialState = {
     userData:{
     accessToken: '',
     userId: '',
     userName: '',
     email: '',
     imageUrl: '',
     roles: [],
     isLogin: false
     }
 }


export default function (state = initialState, action) {

   debugger
    switch (action.type) {
        case LOGIN:

            localStorage.setItem('userData', JSON.stringify(action.payload))
            return{
                ...state,
                userData: action.payload
            }
        case LOGIN_ERROR:
            console.log("LOGIN ERROR");
            return{
                ...state,
                userData: initialState.userData
            }
        default:
             return state
    }
}