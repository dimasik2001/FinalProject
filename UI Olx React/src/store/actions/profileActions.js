import store from '../store';
import { PROFILE_ERROR, UPDATE_PROFILE, UNAUTHORIZED, GET_PROFILE} from '../types'
import axios from 'axios'
import host from './host/hostName'
import { auth, authWithContentType } from '../requestConfigs/requestConfig';
import profile from '../../component/profile';

export const updateProfile = (profileModel) => async dispatch => {
    try {
        debugger

        if(profileModel.image != null)
        {
            let formData = new FormData();
            formData.append("image", profileModel.image);

            await axios.post(`${host}api/images/profile`, formData, authWithContentType)
        }
        const res = await axios.post(`${host}api/users`, profileModel, auth)
        dispatch({
            type: UPDATE_PROFILE,
            payload: res.data
        })
    }
    catch (e) {
        if (e.response.status === 401) {
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
        }
        dispatch({
            type: PROFILE_ERROR,
            payload: console.log(e),
        })
    }
}

export const getProfile = () => async dispatch => {
    try {
        debugger
        let userDataJson= localStorage.getItem('userData');
        let userData = JSON.parse(userDataJson)
        const res = await axios.get(`${host}api/users/${userData.userId}`)
        dispatch({
            type: GET_PROFILE,
            payload: res.data
        })
    }
    catch(e) {
        }
        dispatch({
            type: PROFILE_ERROR,
        })
    }

