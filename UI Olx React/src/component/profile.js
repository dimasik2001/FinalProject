import React, { Component } from 'react'
import { connect } from 'react-redux'
import { updateProfile, getProfile } from '../store/actions/profileActions'
import host from '../store/actions/host/hostName'
import { Form, Row, Col, Button } from 'react-bootstrap'
import imageToBase64 from 'image-to-base64/browser';
import profileReducer from '../store/reducers/profileReducer';
class profile extends Component {
    constructor(props) {
        super(props);
        this.onChangeEmail = this.onChangeEmail.bind(this);
        this.onChangePassword = this.onChangePassword.bind(this);
        this.onChangeNewPassword = this.onChangeNewPassword.bind(this);
        this.onSaveChanges = this.onSaveChanges.bind(this);
        this.onChangeUserName = this.onChangeUserName.bind(this);
        this.onImageChange = this.onImageChange.bind(this);
        this.setState = this.setState.bind(this);
    }

    onChangeUserName(e) {
        debugger
        this.setState({ userName: e.target.value });
    }
    onChangeEmail(e) {
        this.setState({ email: e.target.value });
    }

    onChangePassword(e) {
        this.setState({ password: e.target.value });
    }
    onChangeNewPassword(e) {
        this.setState({ newPassword: e.target.value });
    }
    onSaveChanges() {
        debugger
        this.props.updateProfile({ userName: this.state?.userName, 
            email: this.state?.email,
             password: this.state?.password, 
             newPassword:this.state?.newPassword,
             image: this.state?.image});
    }
    onImageChange(e) {
        if (e.target.files && e.target.files[0]) {
            let img = e.target.files[0];
            this.setState({
                image: img
            });
           
        }
    }
    componentDidMount() {
        this.props.getProfile();
    }


    render() {
        debugger
        const {profileData} = this.props.profile;
        const retrievedStoreStr = localStorage.getItem('userData') 
        const userData = JSON.parse(retrievedStoreStr) 
        let image
        if(profileData.imagePath === null||profileData.imagePath === '')
        {
            image = `${host}unset/profile.png`
        }
        else
        {
          image = host + profileData.imagePath
        }
        if (userData.isLogin && profileData.id !== '') {
            
            return (<div class="container rounded bg-white mt-5 mb-5">
            <div class="row">
                <div class="col-md-3 border-right">
                    <div class="d-flex flex-column align-items-center text-center p-3 py-5">
                        <img class="rounded-circle mt-5" width="150px" height = "150px" src={image}/>
                        <span class="font-weight-bold">
                            {profileData.userName}</span><span class="text-black-50">{profileData.email}<br/>{profileData.id}
                            </span><span> </span></div>
                </div>
                <div class="col-md-5 border-right">
                    <div class="p-3 py-5">
                        <div class="d-flex justify-content-between align-items-center mb-3">
                            <h4 class="text-right">Profile Settings</h4>
                        </div>
                        <Form className="mt-5">
                <Form.Group as={Row} className="mb-3">

                    <Col sm="10">
                        <Form.Control placeholder="UserName" onChange={this.onChangeUserName}/>
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3" controlId="formPlaintextEmail">
                
                    <Col sm="10">
                        <Form.Control placeholder="Email" onChange={this.onChangeEmail}  />
                    </Col>
                </Form.Group>

                <Form.Group as={Row} className="mb-3" controlId="formPlaintextPassword">

                    <Col sm="10">
                        <Form.Control type="password" placeholder="Password" onChange={this.onChangePassword} />
                    </Col>
                    <Col sm="10">
                        <Form.Control type="password" placeholder="New password" onChange={this.onChangeNewPassword} />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3" controlId="formPlaintextPassword">
      
                    <Col sm="10">
                        <Form.Control type="file" onChange={this.onImageChange}/>
                    </Col>
                </Form.Group>
                <Col sm="10">
                <Button variant="primary" sm = "10" onClick={this.onSaveChanges}>
                    Save changes</Button>
                    </Col>
            </Form>
                    </div>
                </div>
            </div>
        </div>
     )
        }

        return (
            <>
                <h>You are not authorized</h>
            </>
        )
    }
}
const mapStateToProps = (state) => ({ profile: state.profile });
export default connect(mapStateToProps, { updateProfile, getProfile })(profile);
