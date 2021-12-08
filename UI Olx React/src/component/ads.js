import React, { Component } from 'react'
import { connect } from 'react-redux'
import { getAds, updateAds, updateImages, deleteImages, deleteAd, createAd } from '../store/actions/adsActions'
import host from '../store/actions/host/hostName'
import { Card, Button, Row, Col, Pagination } from 'react-bootstrap'
import AdEditor from './adEditor'
import categories from '../store/categories/categories'
class ads extends Component {

    constructor(props) {
        super(props);
        this.onEditClick = this.onEditClick.bind(this)
        this.onUpdate = this.onUpdate.bind(this);
        this.onDelete = this.onDelete.bind(this);
        this.onCreate = this.onCreate.bind(this);
        this.onCloseEditor = this.onCloseEditor.bind(this);
        this.renderEditButton = this.renderEditButton.bind(this);
        this.onAddClick = this.onAddClick.bind(this)
    }
    componentDidMount() {
        if(this.props.displayMode == "my"){
        this.props.getAds("api/users/Myads");
        }
        else{
        this.props.getAds("api/ads");
        }
        this.convertDate = this.convertDate.bind(this);
    }
    convertDate(datestr) {
        const date = new Date(datestr);
        return `${date.toDateString()}`
    }
    onEditClick(ad) {
        debugger
        this.setState({ isOnEdit: true, selectedAd: ad, isOnCreate: false })
    }
    onUpdate(ad) {
        debugger
        if (ad?.deletedImages !== undefined && ad?.deletedImages.length > 0) {
            this.props.deleteImages(ad.deletedImages, ad.id)
        }
        if (ad?.newImages !== undefined && ad.newImages.length > 0) {
            this.props.updateImages(ad.newImages, ad.id)
        }
        this.props.updateAds(ad);

        this.setState({ isOnEdit: false })
    }
    onDelete(ad) {
        this.props.deleteAd(ad)
        this.setState({ isOnEdit: false })
    }
    onCreate(ad) {
        debugger
        this.props.createAd(ad);
        this.setState({ isOnCreate: false })
    }
    onCloseEditor() {
        this.setState({ isOnEdit: false, isOnCreate: false })
    }

    onAddClick() {
        this.setState({ isOnCreate: true, isOnEdit: false })
    }
    renderEditButton(ad) {
        const displayMode = this.props.displayMode
        if (displayMode === "my") {
            return <Button variant="dark" onClick={() => this.onEditClick(ad)}>Edit</Button>
        }
    }

    createQueryString(pageNumber) {
        let query = new URLSearchParams(window.location.search);
        query.set("page", pageNumber)
        return query.toString();
    }
    takeShortDescription(str) {
        if(str == null || str == undefined)
        return ''
        let dots = str?.length > 47 ? '...' : '';
        return str?.substring(0, 46) + dots
    }

    render() {
        debugger
        const displayMode = this.props.displayMode
        const { paginationParameters } = this.props.ads;
        const { ads } = this.props.ads;

        let display
        let addComponent
        let pagination;

        let active = paginationParameters.page;
        let totalPages = Math.ceil(paginationParameters.total / paginationParameters.pageSize)
        let items = [];
        for (let number = 1; number <= totalPages; number++) {
            items.push(
                <Pagination.Item key={number} href={window.location.pathname + "?" + this.createQueryString(number)} active={number === active}>
                    {number}
                </Pagination.Item>,
            );
        }

        pagination = (
            <div>
                <Pagination>{items}</Pagination>
            </div>
        );
        if (displayMode === "my") {
            addComponent = <Button variant="success" onClick={this.onAddClick} size="lg">+</Button>
        }
        if (this.state?.isOnEdit) {
            debugger
            display = <div><AdEditor displayMode="update" ad={this.state.selectedAd} onUpdate={this.onUpdate} onDelete={this.onDelete} onClose={this.onCloseEditor}></AdEditor></div>
        }
        else if (this.state?.isOnCreate) {
            display = <div><AdEditor displayMode="create" onCreate={this.onCreate} onClose={this.onCloseEditor}></AdEditor></div>
        }
        else {
            display = <div>
                <i className = "d-flex flex-row mt-3 mb-3">{paginationParameters.total} ads was found</i>
                <Row md={3} className="g-3">
                    {ads?.map(a =>
                        <Col>
                            <Card>
                                <Card.Link href={`/ads/${a.id}`}>
                                    <Card.Img variant="top" src={a.images[0] ?? `${host}unset/ad.png`} height="300" />
                                </Card.Link>
                                <Card.Body>
                                    <Card.Title>{a.header}</Card.Title>
                                    <Card.Text>
                                        <b>{a.price}$</b><br />
                                        {this.takeShortDescription(a.description)}<br />
                                    </Card.Text>
                                    {this.renderEditButton(a)}
                                </Card.Body>
                                <Card.Footer className="text-muted">{this.convertDate(a.changeDate)}</Card.Footer>

                            </Card>
                        </Col>
                    )} </Row>{addComponent} {pagination}</div>
        }


        debugger
        return (<>
            {display}
        </>)
    }
}

const mapStateToProps = (state) => ({ ads: state.ads });

export default connect(mapStateToProps, { getAds, updateAds, updateImages, deleteImages, deleteAd, createAd })(ads);