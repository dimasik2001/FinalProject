import React, { Component } from 'react'
import { connect } from 'react-redux'
import { getUsers, deleteUser } from '../store/actions/usersActions'
import host from '../store/actions/host/hostName'

class users extends Component {
    componentDidMount() {
       debugger
        this.props.getUsers();
    }

    ondeleteUser(id) {
        this.props.deleteUser(id);
    }

    render() {
        debugger
        const { users } = this.props.users
        console.log(users)
        const retrievedStoreStr = localStorage.getItem('userData') // this is a string
        const userData = JSON.parse(retrievedStoreStr) // this will be a JSON object
        
        debugger
        const image = userData?.imageUrl;
        const userName = userData?.userName;
        const isLogin = userData.isLogin.toString();

        return (
            <div>
                {users.map(u => 
                     <React.Fragment key={u.id}>
                         <h6 >{u.firstName} {u.lastName}</h6> 
                     <button onClick={()=>this.ondeleteUser(u.id)}>-</button>
                     </React.Fragment>
                )}
                <h6 >{userName}</h6>
                <p>isLogin = {isLogin}</p>
                <img src = {host+image}></img>
            </div>
        )
    }
}

const mapStateToProps = (state) => ({ users: state.users });

export default connect(mapStateToProps, { getUsers, deleteUser })(users);