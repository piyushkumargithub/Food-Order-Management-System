import { Button, Form ,Alert} from "react-bootstrap";
import React, { useState } from "react";
import { useNavigate } from "react-router-dom";
import {Variable} from "../Variable";



function Login() {
  const [username, setusername] = useState("");
  const [password, setpassword] = useState("");
  const [Error, setError] = useState(false);
  
  const navigate = useNavigate();

  async function login() {
    // console.warn(username, password);
    let credentials = { username: username, password: password };
    try {
      let result = await fetch(Variable.AUTH_API_URL+"login", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
        body: JSON.stringify(credentials),
      });
      result = await result.json();
      // console.log(result);
      if (result?.tokenString.length > 1) {
        localStorage.setItem("token", JSON.stringify(result?.tokenString));
        localStorage.setItem("userid", JSON.stringify(result?.userId));
        localStorage.setItem("username", JSON.stringify(result?.userName));
        setError(false);
        navigate("/foodordercart");
      }
    } catch (err) {
      console.log(err);
      
      setError(true);
      
    }
  }

  return (
    <div>
      {Error?<Alert variant="danger" onClose={() => setError(false)} dismissible>
        <Alert.Heading>Oh snap!</Alert.Heading>
        <p>
         Incorrect Username or Password
        </p>
      </Alert>:<></>}

    <div style={{paddingTop:"5%"}}>
      <h1 className="text-center">Login Page</h1>
      <Form className="col-lg-4 offset-4" style={{padding:"20px",border:"2px solid black",borderRadius:"25px"}}>
        <Form.Group className="mb-3" controlId="formBasicUsername">
          <Form.Label>Username</Form.Label>
          <Form.Control
            required
            type="text"
            placeholder="Username"
            onChange={(e) => setusername(e.target.value)
            }
            
          />

          <Form.Text className="text-muted">Welcome to our new app</Form.Text>
        </Form.Group>

        <Form.Group className="mb-3" controlId="formBasicPassword">
          <Form.Label>Password</Form.Label>
          <Form.Control
            required
            type="password"
            placeholder="Password"
            onChange={(e) => setpassword(e.target.value) }
          />
        </Form.Group>

        <Button onClick={login} variant="primary offset-5">
          Login
        </Button>
      </Form>
      </div>
    </div>
  );
}

export default Login;
