import React from 'react'
import { connect } from 'react-redux'
import { register } from '../store/actions/registerFormActions'
import imageToBase64 from 'image-to-base64/browser';
import { Form, Row, Col, Button } from 'react-bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css';

class registerForm extends React.Component {

    constructor(props) {
        super(props);
        this.onChangeEmail = this.onChangeEmail.bind(this);
        this.onChangePassword = this.onChangePassword.bind(this);
        this.onRegister = this.onRegister.bind(this);
        this.onChangeUserName = this.onChangeUserName.bind(this);
        this.setState = this.setState.bind(this);
    }

    onChangeUserName(e) {
        this.setState({ userName: e.target.value });
    }
    onChangeEmail(e) {
        this.setState({ email: e.target.value });
    }

    onChangePassword(e) {
        this.setState({ password: e.target.value });
    }

    onRegister() {
        debugger
        this.props.register({ userName: this.state?.userName, email: this.state?.email, password: this.state?.password});
    }
    
    

    render() {
        return <>
            <Form className="mt-5">
                <Form.Group as={Row} className="mb-3">
                    <Form.Label column sm="5">
                        UserName
                    </Form.Label>
                    <Col sm="3">
                        <Form.Control placeholder="UserName" onChange={this.onChangeUserName} />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} className="mb-3" controlId="formPlaintextEmail">
                    <Form.Label column sm="5">
                        Email
                    </Form.Label>
                    <Col sm="3">
                        <Form.Control placeholder="Email" onChange={this.onChangeEmail} />
                    </Col>
                </Form.Group>

                <Form.Group as={Row} className="mb-3" controlId="formPlaintextPassword">
                    <Form.Label column sm="5">
                        Password
                    </Form.Label>
                    <Col sm="3">
                        <Form.Control type="password" placeholder="Password" onChange={this.onChangePassword} />
                    </Col>
                </Form.Group>
                <Button variant="primary" onClick={this.onRegister}>
                    Register now!
                </Button>
            </Form>
        </>
    }

}

//const mapStateToProps = (state) => ({ registerForm: state.message });

export default connect(null, { register })(registerForm);