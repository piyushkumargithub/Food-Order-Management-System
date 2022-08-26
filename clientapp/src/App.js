import {Route, Routes} from "react-router-dom";
import FoodOrderCart from "./component/FoodOrderCart";
import Login from "./component/Login";
import History  from "./component/History";
import './App.css';

function App() {
  return (
    <>
        <Routes>
          <Route path="/" element={<Login/>}/>
          
          <Route path="foodordercart" element={<FoodOrderCart/>}/>
          <Route path="orderhistory" element={<History/>}/>
        </Routes>
        
      </>
  );
}

export default App;
