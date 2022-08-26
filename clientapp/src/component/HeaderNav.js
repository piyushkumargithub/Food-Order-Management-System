import React from "react";
import { Navbar, Container, Nav ,Button } from "react-bootstrap";
import { useNavigate} from "react-router-dom";
function HeaderNav() {
  const navigate = useNavigate();

    const logout = async () => {
        // if used in more components, this should be in context 
        // axios to /logout endpoint
        
        localStorage.clear(); 
        navigate('/');
    }
  return (
    <Navbar sticky="top" bg="dark" variant="dark">
      <Container >
        <Navbar.Brand>FoodOrderingApp</Navbar.Brand>
        <Nav className="me-auto">
          <Nav.Link href="/foodordercart">Order Page</Nav.Link>
          <Nav.Link href="/orderhistory">Order History</Nav.Link>
          
        </Nav>
        <Nav>
          <Nav.Link >
          {localStorage.getItem("username").replaceAll('"',"")}
          </Nav.Link>
          <Button onClick={logout}>Sign Out</Button>
        </Nav>
      </Container>
    </Navbar>
  );
}

export default HeaderNav;
