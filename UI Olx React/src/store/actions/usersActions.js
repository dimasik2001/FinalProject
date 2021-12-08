import store from '../store';
import {GET_USERS, USERS_ERROR, DELETE_USER} from '../types'
import axios from 'axios'
import host from './host/hostName'

export const getUsers = () => async dispatch => {
    try{
         const res = await axios.get(`${host}api/users/37053a73-b3f3-4f94-ad23-8a381763231f`)
         dispatch({
             type: GET_USERS,
             payload: res.data
         })
    }
    catch(e){
        dispatch({
            type: USERS_ERROR,
            payload: console.log(e),
        })
    }
}

export const deleteUser = (id) => async (dispatch) => {
    try{
 
        let state = store.getState();
        let accessToken = state.loginForm.userData.accessToken;

        await axios.delete(`${host}Customers/${id}`,{
            headers: {
              Authorization: `Bearer ${accessToken}`
            }
        })

        dispatch({
            type: DELETE_USER,
            payload: id
        })
    }
    catch(e){
        dispatch({
            type: USERS_ERROR,
            payload: console.log(e),
        })
    }
}