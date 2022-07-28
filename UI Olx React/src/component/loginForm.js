import React, { Component } from 'react'
import { connect } from 'react-redux'
import { login } from '../store/actions/loginFormActions'

import { Form, Row, Col, Button } from 'react-bootstrap'
import 'bootstrap/dist/css/bootstrap.min.css';

class loginForm extends Component {
    /*componentDidMount() {
        debugger;
        this.props.login({email:"dimon228@gmail.com", password: "Dimon228@gmail.com"});
    }*/

    constructor(props) {
        super(props);
        this.onChangeLogin = this.onChangeLogin.bind(this);
        this.onChangePassword = this.onChangePassword.bind(this);
        this.onLogin = this.onLogin.bind(this);
    }

    onChangeLogin(e) {
        this.setState({ login: e.target.value });
    }

    onChangePassword(e) {
        this.setState({ password: e.target.value });
    }

    onLogin() {
        //let state = store.getState();
        //console.log(state.loginForm.userData.accessToken);
        this.props.login({ email: this.state.login, password: this.state.password });
    }

    render() {
        let message = this.props?.message;
        return (
            <>
            <Form className="mt-5">
                <Form.Group as={Row} className="mb-3" controlId="formPlaintextEmail">
                    <Form.Label column sm="5">
                        Email
                    </Form.Label>
                    <Col sm="3">
                        <Form.Control placeholder="Email" onChange={this.onChangeLogin} />
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
               <Button variant="primary" onClick={this.onLogin}>Login</Button>
            </Form>     
            {message}    
            </>   
        )
    }

}

const mapStateToProps  = (state) => ({message: state?.loginForm?.message});
export default connect(mapStateToProps, {login} )(loginForm);