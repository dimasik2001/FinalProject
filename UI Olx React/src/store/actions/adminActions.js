import { GET_USERS, DELETE_USER, USERS_ERROR} from '../types'
import { auth } from '../requestConfigs/requestConfig'
import axios from 'axios'
import host from './host/hostName'
export const deleteUser = (id) => async (dispatch) => {
    try{
        debugger
        await axios.delete(`${host}api/users/${id}`,auth)

        dispatch({
            type: DELETE_USER
        })
    }
    catch(e){
        dispatch({
            type: USERS_ERROR,
            payload: console.log(e),
        })
    }
}