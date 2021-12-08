import { GET_ADS, ADS_ERROR, UPDATE_AD, CREATE_AD, UNAUTHORIZED, IMAGES_DELETED, DELETE_AD, GET_AD, AD_ERROR } from '../types'
import axios from 'axios'
import host from './host/hostName'
import { auth, authWithContentType } from '../requestConfigs/requestConfig'
export const getAds = (apiPath) => async dispatch => {
    try {
        debugger
        const query = new URLSearchParams(window.location.search);
        const queryStr = query.toString();
        const res = await axios.get(`${host}${apiPath}?${queryStr}`,auth)
        dispatch({
            type: GET_ADS,
            payload: res.data
        })
    }
    catch (e) {
        if (e.response?.status === 401) {
            window.location.href = '/login';
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
        }
        dispatch({
            type: ADS_ERROR,
            payload: console.log(e),
        })
    }
}
export const getAdInfoById = (id) => async dispatch => {
    try {
        debugger
        const resAd = await axios.get(`${host}api/ads/${id}`)
        const resUser = await axios.get(`${host}api/users/${resAd.data.userId}`)
        dispatch({
            type: GET_AD,
            payload: { user: resUser.data, ad: resAd.data }
        })
    }
    catch (e) {
        dispatch({
            type: AD_ERROR,
            payload: console.log(e),
        })
    }
}

export const updateAds = (ad) => async dispatch => {
    try {
        debugger
        const res = await axios.put(`${host}api/ads/${ad.id}`,
            { header: ad.header, description: ad.description, price: ad.price, categories: ad.categories },
            auth
        )
        dispatch({
            type: UPDATE_AD,
            payload: res.data
        })
    }
    catch (e) {
        if (e.response.status === 401) {
            window.location.href = '/login'
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
            dispatch({
                type: ADS_ERROR,
                payload: console.log(e),
            })
        }
    }
}

export const createAd = (ad) => async dispatch => {
    try {
        let res = await axios.post(`${host}api/ads/`,
            { header: ad.header, description: ad.description, price: ad.price, categories: ad.categories },
            auth
        )
        let createdAd = res.data;
        debugger
        if (ad.newImages !== undefined && ad.newImages.length > 0) {
            let formData = new FormData();
            for (let i = 0; i < ad.newImages.length; i++) {
                formData.append("images", ad.newImages[i]);
            }
            await axios.post(`${host}api/images/ads/${createdAd.id}`,
                formData,
                authWithContentType
            )
        }
        window.location.reload()
        dispatch({
            type: CREATE_AD,
            payload: res.data
        })
    }
    catch (e) {
        if (e.response.status === 401) {
            window.location.href = '/login'
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
            dispatch({
                type: ADS_ERROR,
                payload: console.log(e),
            })
        }
    }
}

export const deleteAd = (ad) => async dispatch => {
    try {
        debugger
        await axios.delete(`${host}api/ads/${ad.id}`,
            auth
        )
        dispatch({
            type: DELETE_AD,
            payload: ad
        })
    }
    catch (e) {
        if (e.response?.status === 401) {
            window.location.href = '/login'
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
            dispatch({
                type: ADS_ERROR,
                payload: console.log(e),
            })
        }
    }
}


export const updateImages = (images, id) => async dispatch => {
    try {
        debugger
        let formData = new FormData();
        for (let i = 0; i < images.length; i++) {
            formData.append("images", images[i]);
        }
        await axios.post(`${host}api/images/ads/${id}`,
            formData,
            authWithContentType
        )
    }
    catch (e) {
        if (e.response.status === 401) {
            window.location.href = '/login'
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
            dispatch({
                type: ADS_ERROR,
                payload: console.log(e),
            })
        }
    }
}

export const deleteImages = (images, id) => async dispatch => {
    try {
        debugger
        await axios.post(`${host}api/images/ads/delete/${id}`,
            images,
            auth)
        dispatch({
            type: IMAGES_DELETED,
            payload: { images, id }
        })

    }
    catch (e) {
        if (e.response?.status === 401) {
            window.location.href = '/login'
            dispatch({
                type: UNAUTHORIZED,
                payload: console.log(e),
            })
            dispatch({
                type: ADS_ERROR,
                payload: console.log(e),
            })
        }
    }
}