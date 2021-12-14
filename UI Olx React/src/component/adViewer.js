import React, { Component } from 'react'
import { deleteAd } from '../store/actions/adsActions'
import { connect } from 'react-redux'
import { getAdInfoById } from '../store/actions/adsActions'
import host from '../store/actions/host/hostName'
import { Card, Button, CardGroup, Row, Col, Carousel } from 'react-bootstrap'
import AdEditor from './adEditor'
import categories from '../store/categories/categories'
import 'photoswipe/dist/photoswipe.css'
import '../styles.css'
import 'photoswipe/dist/default-skin/default-skin.css'
import { Gallery, Item } from 'react-photoswipe-gallery'
import {saveToFavourite} from '../store/actions/cookiesAction'
import { blockUser } from '../store/actions/adminActions'
import { Link } from 'react-router-dom'

class adViewer extends Component {

    constructor(props) {
        super(props);
        this.onFavClick = this.onFavClick.bind(this)
        this.onBlockUserClick = this.onBlockUserClick.bind(this)
        this.onDeleteAdClick = this.onDeleteAdClick.bind(this)
    }

    componentDidMount() {
        this.props.getAdInfoById(this.props.match.params.id)
    }
    onFavClick(e){
        debugger
        saveToFavourite(this.props.adViewer.ad);
    }
    onBlockUserClick(isBlocked){
        debugger
        this.props.blockUser(this.props.adViewer.user.id, isBlocked)
    }
    onDeleteAdClick(){
        this.props.deleteAd(this.props.adViewer.ad)
    }
    render() {
        debugger
        const { ad, user } = this.props.adViewer;
        let imagePath;
        if(ad == null||ad==undefined || ad==''){
            return <div>Ad does non exist</div>
        }
        if(user?.imagePath == undefined|| user?.imagePath == null || user?.imagePath =='')
        {
            imagePath = 'unset/profile.png'
        }
        else
        {
            imagePath = user.imagePath;
        }
        const retrievedStoreStr = localStorage.getItem('userData') 
        const userData = JSON.parse(retrievedStoreStr) 
        let blockUserButton
        let unBlockUserButton
        let deleteAdButton
        if(userData.roles.find((r) => r =="Admin") != undefined)
        {
            blockUserButton = <Button variant = "danger" onClick={() => this.onBlockUserClick(true)}> Block</Button>
            unBlockUserButton = <Button variant = "success" onClick={() => this.onBlockUserClick(false)}> Unlock</Button>
            deleteAdButton = <Button variant = "danger" onClick={this.onDeleteAdClick}> Delete </Button>
        }
        return (
            <div
                style={{
                    display: 'inline'
                }}
            >
                {/* <i>{ad?.categories}</i> */}
<Gallery>
                        {ad?.images?.map((i) =>
                            <Item
                                original={host + i}
                                thumbnail={host + i}
                                width="1024"
                                height="768"
                            >
                                {({ ref, open }) => (
                                    <img ref={ref} onClick={open} src={host + i} />
                                )}
                            </Item>)}
                    </Gallery>
                <div className="product-card">
                    
                    <h1>{ad?.header}</h1>
                    <p class="price">${ad?.price}</p>
                    <p>{ad?.description}</p>
                    <i>{ad?.categories.map((c)=><>{`${c.name}/ `}</>)}</i>
                    {/* <p><button className = "fav-button" onClick = {this.onFavClick}>Add to Favourites</button></p> */}
                    <p><Button  variant = "dark"onClick = {this.onFavClick}>Add to Favourites</Button>{deleteAdButton}</p>
                </div>
                <div className="profile-card">
                    <Link to ={ `/ads?filterItem=userId&itemId=${user?.id}`}>
                    <img src = {host + imagePath}></img>
                    </Link>
                    <div>
                        <i>User name: </i> <b>{user?.userName}</b><br/>
                        <i>Email: </i> <b>{user?.email}</b><br/>
                           {user?.id}<br/>
                           {blockUserButton}
                           {unBlockUserButton}
                    </div>
                </div>
            </div>

            
        )
    }
}
const mapStateToProps = (state) => ({ adViewer: state.adViewer });

export default connect(mapStateToProps, { getAdInfoById, blockUser,deleteAd })(adViewer);