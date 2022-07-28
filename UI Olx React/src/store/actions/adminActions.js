import { USERS_ERROR} from '../types'
import { auth } from '../requestConfigs/requestConfig'
import axios from 'axios'
import host from './host/hostName'
export const blockUser = (id, isBlocked) => async (dispatch) => {
    try{
        debugger
        await axios.put(`${host}api/users/${id}/${isBlocked}`,null,auth)

        dispatch({
        })
    }
    catch(e){
        dispatch({
            type: USERS_ERROR,
            payload: console.log(e),
        })
    }
}