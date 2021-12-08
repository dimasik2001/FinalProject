import React, { Component } from 'react'
import { connect } from 'react-redux'
import { getAds } from '../store/actions/adsActions'
import { Button, Image, ListGroup, Col, Form } from 'react-bootstrap'
import host from '../store/actions/host/hostName'
import categories from '../store/categories/categories'
class ads extends Component {

    constructor(props) {
        super(props);
        this.onCheckChange = this.onCheckChange.bind(this);
        this.defineCheck = this.defineCheck.bind(this);
        this.onHeaderChange = this.onHeaderChange.bind(this);
        this.onDescriptionChange = this.onDescriptionChange.bind(this);
        this.onPriceChange = this.onPriceChange.bind(this);
        this.onUpdateClick = this.onUpdateClick.bind(this);
        this.onDeleteClick = this.onDeleteClick.bind(this);
        this.onCreateClick = this.onCreateClick.bind(this);
        this.onImagesChange = this.onImagesChange.bind(this);
        this.onDeleteImage = this.onDeleteImage.bind(this);
        this.imageToDeleteList = this.imageToDeleteList.bind(this);
    }
    componentDidMount() {
        if (this.props.ad !== undefined) {
            this.setState({
                id: this.props.ad.id,
                header: this.props.ad.header,
                description: this.props.ad.description,
                categories: this.props.ad.categories,
                images: this.props.ad.images,
                price: this.props.ad.price
            })
        }
        else{
            this.setState({categories: []})
        }
    }

    defineCheck(id) {
        debugger
        if (this.state === null) {
            return false
        }
        return this.state.categories?.find(c => c.id === id) !== undefined
    }

    onCheckChange(e) {
        debugger
        let checkBox = e.target;
        let adCategories = this.state.categories
        const category = categories.find(c => c.id.toString() === checkBox.id)
        if (checkBox.checked) {
            if (adCategories.find(c => c.id === category.id) === undefined) {
                adCategories.push(category)
            }
        }
        if (!checkBox.checked) {
            adCategories = adCategories.filter(c => c.id !== category.id)
        }
        this.setState({ categories: adCategories })
    }
    onImagesChange(e) {
        debugger
        if (e.target.files && e.target.files[0]) {
            let imgs = e.target.files;
            this.setState({
                newImages: imgs
            })
        }
    }
    onHeaderChange(e) {
        this.setState({ header: e.target.value })
    }
    onDescriptionChange(e) {
        this.setState({ description: e.target.value })
    }
    onPriceChange(e) {
        this.setState({ price: e.target.value })
    }
    onUpdateClick() {
        const updatedAd = this.state
        this.props.onUpdate(updatedAd)
    }

    onDeleteClick() {
        const deletedAd = this.state;
        this.props.onDelete(deletedAd)
    }
    onCreateClick() {
        const createdAd = this.state
        this.props.onCreate(createdAd)
    }
    imageToDeleteList(path) {
        let toDelete = this.state?.deletedImages;
        if (toDelete === undefined || toDelete === null) {
            toDelete = []
        }
        toDelete.push(path.replace(host, ''));
        this.setState({ deletedImages: toDelete })
    }
    onDeleteImage(e) {
        debugger
        var image = e.target.id.toString()
        let filterImages = this.state.images;
        filterImages = filterImages.filter(i => i !== image)
        this.imageToDeleteList(image);
        this.setState({ images: filterImages });
    }
    render() {
        let updateButton
        let deleteButton
        let createButton
        debugger
        let ad = this.state;
        if (this.props.displayMode === "update") {
            updateButton = <Button onClick={this.onUpdateClick} variant="primary">Update</Button>
            deleteButton = <Button onClick={this.onDeleteClick} variant="danger">Delete</Button>
        }
        else if (this.props.displayMode === "create") {
            createButton = <Button onClick={this.onCreateClick} variant="success">Create</Button>
        }
        return (<> <div className="d-flex flex-row-reverse mb-3 ml-3"><Button variant="danger" size="lg" onClick={this.props.onClose}>X</Button></div>
            <ListGroup horizontal> {
                ad?.images?.map(i => <>
                    <ListGroup.Item><Image src={i} height="150" width="200"></Image> <Button onClick={this.onDeleteImage} id={i} variant="danger">-</Button></ListGroup.Item>
                </>)
            }


            </ListGroup>
            <Col sm="3" className="mt-3">
                <Form.Control type="file" multiple onChange={this.onImagesChange} />
            </Col>
            <ListGroup className="text-left">
                <ListGroup.Item >
                    <Form.Control type="text" placeholder="Header" className="mb-3" value={ad?.header} onChange={this.onHeaderChange} />
                </ListGroup.Item>
                <ListGroup.Item >
                    <Form.Control type="number" placeholder="Price" className="mb-3" value={ad?.price} onChange={this.onPriceChange} />
                </ListGroup.Item>
                <ListGroup.Item >
                    <Form.Control as="textarea" placeholder="Descriprion" rows={10} className="mb-3" value={ad?.description} onChange={this.onDescriptionChange} />
                </ListGroup.Item>
                <ListGroup.Item>
                    {categories.map(c => <> <Form.Check inline label={c.name} id={c.id} onChange={this.onCheckChange} checked={this.defineCheck(c.id)} /></>)}
                </ListGroup.Item>
            </ListGroup>
            <div className="mb-3 mt-3">
                {updateButton}{deleteButton}{createButton}
            </div>
        </>)
    }
}

const mapStateToProps = (state) => ({ adEditor: state.adEditor });

export default connect(mapStateToProps, { getAds })(ads);
