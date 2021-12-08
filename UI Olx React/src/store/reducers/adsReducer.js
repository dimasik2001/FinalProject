import { GET_ADS, ADS_ERROR, UPDATE_AD, CREATE_AD, UPDATE_PROFILE, IMAGES_DELETED, DELETE_AD } from '../types'
import host from '../actions/host/hostName';
import { act } from 'react-dom/test-utils';
import { Accordion } from 'react-bootstrap';
const initialState = {
    ads: [],
    paginationParameters: { page: '', pageSize: '', totalPages: '' }
}

export default function (state = initialState, action) {
    switch (action.type) {

        case GET_ADS:
            debugger
            let formatedAds = action.payload.ads;
            formatedAds.forEach(ad => {
                if (ad.images !== null) {
                    for (let i = 0; i < ad.images.length; i++) {
                        ad.images[i] = host + ad.images[i];
                    }
                }
            });

            return {
                ...state,
                ads: formatedAds,
                paginationParameters: action.payload.paginationParameters
            }
        case UPDATE_AD:
            debugger
            let ad = action.payload;
             if (ad.images !== null) {
                for (let i = 0; i < ad.images.length; i++) {
                    ad.images[i] = host + ad.images[i];
                }
            }
            let newAds = state.ads.filter(a => a.id !== ad.id)
            newAds.push(ad)
            window.location.reload();
            return {
                ...state,
                ads: newAds
            }

            case IMAGES_DELETED:
            debugger
            let id = action.payload.id;
            let images = action.payload.images;
             if (images !== null) {
                for (let i = 0; i < images.length; i++) {
                    images[i] = host + ad.images[i];
                }
            }
            let currentAd = state.ads.find(a => a.id == id)
            let filterImages = currentAd.images.filter(i => !images.includes(i));
            ad.images = filterImages;
            return {
                ...state,
                ads: newAds
            }

            case DELETE_AD:
            debugger
            let deletedId = action.payload.id;
            return {
                ...state,
                ads: this.state.ads.filter(a => a.id !== deletedId)
            }
            case CREATE_AD:
                debugger
                return {
                    ...state,
                }

        default: return state
    }
}