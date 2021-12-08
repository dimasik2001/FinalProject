import {REGISTER, REGISTER_ERROR} from '../types'
import axios from 'axios'
import host from './host/hostName'
export const register = (personData) => async dispatch => {
    try{
        
        const res = await axios.post(`${host}api/auth/register`, personData)
        dispatch({
            type: REGISTER,
            payload: res.data
        })
        window.location.href = '/login';
    }
    catch(e){
        dispatch({
            type: REGISTER_ERROR,
            payload: console.log(e),
        })
    }
}