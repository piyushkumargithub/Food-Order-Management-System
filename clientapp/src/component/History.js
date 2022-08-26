import React, { useEffect, useState } from "react";
//import axios from "axios";
import { Table, Button } from "react-bootstrap";
import HeaderNav from "./HeaderNav";
import FoodOrderCard from "./Layout/FoodOrderCard";
import UnauthorizedAccess from "./UnauthorizedAccess";
function History() {
  const [UserId, setUserId] = useState(localStorage.getItem("userid"));
  const [FoodOrderHistoryList, setFoodOrderHistoryList] = useState([]);

  useEffect(() => {
    const search = `https://localhost:44306/api/Cart/${localStorage.getItem(
      "userid"
    )}`;
    fetch(search, {
      headers: {
        Authorization:
          "Bearer " + localStorage.getItem("token").replaceAll('"', ""),
      },
    })
      .then((response) => response.json())
      .then((json) => setFoodOrderHistoryList(json));
  }, []);

  return localStorage.getItem("token") ?(
  <>
    <HeaderNav/>
    <div className="col-lg-8 offset-lg-2 mt-5">
      <Table striped bordered hover>
        <thead>
          <tr>
          <th className="col-2">Order Id</th>
          <th className="col-6">Food Item</th>
          <th className="col-2">Quantity</th>
          </tr>
        </thead>
        <tbody>
        {Array.isArray(FoodOrderHistoryList) ? (
              FoodOrderHistoryList.map((FoodItems, index) => (
                <FoodOrderCard key={index} FoodItems={FoodItems} />              
              ))
            ) : (
              <div>No food ordered yet.</div>
            )}
        </tbody>
      </Table>
    </div>
  </>
  ): (
    <UnauthorizedAccess />
  );
  
}

export default History;
