import {LOGIN, LOGIN_ERROR} from '../types'
import axios from 'axios'
import host from './host/hostName'

export const login = (userCredentials) => async dispatch => {
    try{
        const res = await axios.post(`${host}api/auth/login`, userCredentials)
        console.log(res);
        dispatch({
            type: LOGIN,
            payload: res.data
        })
        window.location.href = '/ads';
    }
    catch(e){

        dispatch({
            type: LOGIN_ERROR,
            payload: console.log(e),
        })
    }
}
