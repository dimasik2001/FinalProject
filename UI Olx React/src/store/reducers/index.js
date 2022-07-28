import { combineReducers } from 'redux'
import profile from '../../component/profile'
import adsReducer from './adsReducer'
import adViewerReducer from './adViewerReducer'
import loginFormReducer from './loginFormReducer'
import profileReducer from './profileReducer'
import registerFormReducer from './registerFormReducer'
import userReducer from './userReducer'

export default combineReducers({
  users: userReducer,
  loginForm: loginFormReducer,
  registerForm: registerFormReducer,
  ads: adsReducer,
  profile: profileReducer,
  adViewer: adViewerReducer
})