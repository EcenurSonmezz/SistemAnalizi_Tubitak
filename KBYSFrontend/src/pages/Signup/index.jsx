import { useEffect, useState } from "react";
import { signUp } from "./api";
import { useNavigate } from "react-router-dom";
import "./signup.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import signInImage from '../../assets/deneme.png'; // Resmi doğru yola eklediğinizden emin olun

export function SignUp() {
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [passwordRepeat, setPasswordRepeat] = useState("");
  const [apiProgress, setApiProgress] = useState(false);
  const [successMessage, setSuccessMessage] = useState("");
  const [errors, setErrors] = useState({});
  const [generalError, setGeneralError] = useState("");
  const navigate = useNavigate();

  useEffect(() => {
    setErrors((lastError) => ({
      ...lastError,
      username: undefined,
    }));
  }, [username]);

  useEffect(() => {
    setErrors((lastError) => ({
      ...lastError,
      email: undefined,
    }));
  }, [email]);

  useEffect(() => {
    setErrors((lastError) => ({
      ...lastError,
      password: undefined,
    }));
  }, [password]);

  const onSubmit = async (event) => {
    event.preventDefault();
    setSuccessMessage("");
    setGeneralError("");
    setApiProgress(true);

    try {
      await signUp({
        username,
        email,
        password,
      });
      setSuccessMessage("Kayıt başarılı!");
      navigate("/login");
    } catch (axiosError) {
      if (axiosError.response?.data && axiosError.response.data.statusCode === 400) {
        setErrors(axiosError.response.data.errors);
      } else {
        setGeneralError("Beklenmedik bir hata oluştu. Lütfen tekrar deneyin.");
      }
    } finally {
      setApiProgress(false);
    }
  };

  return (
    <div className="sign-up-container">
      <div className="left-div">
        <img src={signInImage} alt="Sign In Illustration" />
      </div>
      <div className="right-div">
        <div className="form-section">
          <div className="card-header bg-primary text-white">
            <h1>Kayıt Ol</h1>
          </div>
          <div className="card-body">
            <form onSubmit={onSubmit}>
              <div className="mb-3">
                <label htmlFor="username" className="form-label">
                  Kullanıcı Adı
                </label>
                <input
                  id="username"
                  className="form-control"
                  value={username}
                  onChange={(event) => setUsername(event.target.value)}
                />
                {errors.username && (
                  <div className="alert alert-danger mt-2">{errors.username}</div>
                )}
              </div>
              <div className="mb-3">
                <label htmlFor="email" className="form-label">
                  E-mail
                </label>
                <input
                  id="email"
                  type="email"
                  className="form-control"
                  value={email}
                  onChange={(event) => setEmail(event.target.value.toLowerCase())}
                />
                {errors.email && (
                  <div className="alert alert-danger mt-2">{errors.email}</div>
                )}
              </div>
              <div className="mb-3">
                <label htmlFor="password" className="form-label">
                  Şifre
                </label>
                <input
                  id="password"
                  type="password"
                  className="form-control"
                  value={password}
                  onChange={(event) => setPassword(event.target.value)}
                />
                {errors.password && (
                  <div className="alert alert-danger mt-2">{errors.password}</div>
                )}
              </div>
              <div className="mb-3">
                <label htmlFor="passwordRepeat" className="form-label">
                  Şifre Tekrarı
                </label>
                <input
                  id="passwordRepeat"
                  type="password"
                  className="form-control"
                  value={passwordRepeat}
                  onChange={(event) => setPasswordRepeat(event.target.value)}
                />
              </div>
              {successMessage && (
                <div className="alert alert-success text-center">{successMessage}</div>
              )}
              {generalError && (
                <div className="alert alert-danger text-center">{generalError}</div>
              )}
              <div className="text-center">
                <button 
                type="submit"
                  className="btn"
                  disabled={
                    apiProgress || !password || password !== passwordRepeat
                  }
                >
                  {apiProgress && (
                    <span
                      className="spinner-border spinner-border-sm"
                      aria-hidden="true"
                    ></span>
                  )}
                  Kayıt Ol
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
}
