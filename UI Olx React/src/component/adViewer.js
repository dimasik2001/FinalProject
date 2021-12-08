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
import { deleteUser } from '../store/actions/adminActions'

class adViewer extends Component {

    constructor(props) {
        super(props);
        this.onFavClick = this.onFavClick.bind(this)
        this.onDeleteUserClick = this.onDeleteUserClick.bind(this)
        this.onDeleteAdClick = this.onDeleteAdClick.bind(this)
    }

    componentDidMount() {
        this.props.getAdInfoById(this.props.match.params.id)
    }
    onFavClick(e){
        debugger
        saveToFavourite(this.props.adViewer.ad);
    }
    onDeleteUserClick(){
        debugger
        this.props.deleteUser(this.props.adViewer.user.id)
    }
    onDeleteAdClick(){
        this.props.deleteAd(this.props.adViewer.ad)
    }
    render() {
        debugger
        const { ad, user } = this.props.adViewer;
        if(ad == null||ad==undefined || ad==''){
            return <div>Ad does non exist</div>
        }
        const retrievedStoreStr = localStorage.getItem('userData') 
        const userData = JSON.parse(retrievedStoreStr) 
        let deleteUserButton
        let deleteAdButton
        if(userData.roles.find((r) => r =="Admin") != undefined)
        {
            deleteUserButton = <Button variant = "danger" onClick={this.onDeleteUserClick}> Delete</Button>
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
                    <a href ={ `/ads?filterItem=userId&itemId=${user?.id}`}>
                    <img src = {host + user?.imagePath}></img>
                    </a>
                    <div>
                        <i>User name: </i> <b>{user?.userName}</b><br/>
                        <i>Email: </i> <b>{user?.email}</b><br/>
                           {user?.id}<br/>
                           {deleteUserButton}
                    </div>
                </div>
            </div>

            
        )
    }
}
const mapStateToProps = (state) => ({ adViewer: state.adViewer });

export default connect(mapStateToProps, { getAdInfoById, deleteUser,deleteAd })(adViewer);