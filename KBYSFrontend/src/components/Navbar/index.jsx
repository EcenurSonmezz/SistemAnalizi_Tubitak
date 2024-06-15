import { useContext } from "react";
import { Link } from "react-router-dom";
import { Link as ScrollLink } from 'react-scroll';
import { LoginContext } from "../../state/context";
import "./Navbar.css";
import logo from "../../assets/valence.png";

const Navbar = () => {
  const { id, onLogotSuccess } = useContext(LoginContext);

  const handleLogout = () => {
    onLogotSuccess();
  };

  const userId = localStorage.getItem('loginData') ? JSON.parse(localStorage.getItem('loginData')).id : "0";
  const profileLink = `/profile/${userId}`;

  return (
    <nav className="navbar">
      <div className="container">
        <Link to="/" className="brand">
          <img src={logo} className="brand-logo" />


        </Link>
       
        <div className="menu">
          {/* <Link to="/" className="menu-link">
            Ana Sayfa
          </Link> */}
         
          <ScrollLink to="meal-cards-container" smooth={true} duration={500} className="menu-link">
          Günlük Rapor
          </ScrollLink>
          {/* <Link to="#" className="menu-link">
            Beslenme
          </Link> */}
           <Link to="/recipe" className="menu-link">
            Tarifler
          </Link>
          <Link to="/About" className="menu-link">
            Hakkımızda
          </Link>
        </div>
        <div className="auth-buttons">
          {id !== "0" ? (
            <>
              <Link to={profileLink} className="auth-link">
                Profil
              </Link>
              <button onClick={handleLogout} className="auth-link logout-button">
                Çıkış Yap
              </button>
            </>
          ) : (
            <>
              <Link to="/login" className="auth-link">
                Giriş Yap
              </Link>
              <Link to="/signup" className="auth-link">
                Kayıt Ol
              </Link>
            </>
          )}
        </div>
      </div>
    </nav>
  );
};

export default Navbar;
