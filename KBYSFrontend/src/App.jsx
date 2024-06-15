
import { Outlet } from "react-router-dom";
import "./App.css";
import Navbar from "./components/Navbar";
import Footer from "./components/Footer";
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap/dist/js/bootstrap.bundle.min';
import { LoginationContext } from "../src/state/context";


const App = () => {

  return (
    <LoginationContext>
    <div className="app">
      <Navbar />
      <div className="content">
        <Outlet />
      </div>
      <Footer />
    </div>
    </LoginationContext>
  );
};

export default App;
