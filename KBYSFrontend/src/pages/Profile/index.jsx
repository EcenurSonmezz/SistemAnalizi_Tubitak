/* eslint-disable react-hooks/exhaustive-deps */
import { useState, useEffect } from "react";
import { useParams, useNavigate } from "react-router-dom";
import toastr from 'toastr';
import 'toastr/build/toastr.min.css';
import { FaSearch } from 'react-icons/fa';
import { getUserProfile, getAllergyOptions, updateUserProfile, saveUserAllergies, getDiseaseOptions, saveUserDiseases } from './Api';
import "./Profile.css";
import profileImage from "../../assets/profileImage.png";
import profile1 from "../../assets/hasta.png";
import profile2 from "../../assets/allerji.png";

export const Profile = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const [userInfo, setUserInfo] = useState({});
  const [ setDiseases] = useState([]);
  const [ setAllergies] = useState([]);
  const [allergyOptions, setAllergyOptions] = useState([]);
  const [diseaseOptions, setDiseaseOptions] = useState([]);
  const [selectedAllergies, setSelectedAllergies] = useState([]);
  const [selectedDiseases, setSelectedDiseases] = useState([]);
  const [isEditing, setIsEditing] = useState(false);
  const [editInfo, setEditInfo] = useState({});
  const [searchMessage, setSearchMessage] = useState("");

  useEffect(() => {
    const loginData = JSON.parse(localStorage.getItem('loginData'));
    if (!loginData || loginData.id === '0') {
      toastr.error("Önce giriş yapmanız gerekiyor.");
      setTimeout(() => {
        navigate('/login');
      }, 3000); // 3 saniye sonra giriş sayfasına yönlendir
    } else {
      fetchUserProfile();
    }
  }, []);

  const fetchUserProfile = async () => {
    try {
      const data = await getUserProfile(id);
      console.log("Fetched User Profile:", data);
      setUserInfo(data);
      const userDiseases = data.diseases?.$values.map(a => ({
        id: a.id,
        name: a.name ? a.name : "Unknown"
      })) || [];
      console.log("Kullanıcı Hastalık", userDiseases);
      setDiseases(userDiseases);
      const userAllergies = data.allergies?.$values.map(a => ({
        id: a.id,
        name: a.name ? a.name : "Unknown"
      })) || [];
      setAllergies(userAllergies);
    } catch (error) {
      console.error("Error fetching user profile:", error);
      toastr.error("Kullanıcı verisi alınırken bir hata oluştu.");
    }
  };

  const handleEditToggle = () => {
    setIsEditing(!isEditing);
    setEditInfo(userInfo);
  };

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setEditInfo({ ...editInfo, [name]: value });
  };

  const handleSave = async () => {
    try {
      await updateUserProfile(id, editInfo);
      setUserInfo(editInfo);
      setIsEditing(false);
      toastr.success("Kullanıcı bilgileri başarıyla kaydedildi.");
    } catch (error) {
      console.error("Error saving user info:", error);
      toastr.error("Kullanıcı bilgileri kaydedilirken bir hata oluştu.");
    }
  };

  const handleAllergySearch = async (e) => {
    const query = e.target.value;
    if (query.length >= 3) {
      try {
        const options = await getAllergyOptions(query);
        setAllergyOptions(options);
        if (options.length === 0) {
          setSearchMessage("Alerji bulunamadı");
        } else {
          setSearchMessage("");
        }
      } catch (error) {
        console.error("Error searching allergies:", error);
        setSearchMessage("Alerji arama işlemi sırasında bir hata oluştu.");
      }
    } else {
      setAllergyOptions([]);
      setSearchMessage("");
    }
  };

  const handleDiseaseSearch = async (e) => {
    const query = e.target.value;
    if (query.length >= 3) {
      try {
        const options = await getDiseaseOptions(query);
        setDiseaseOptions(options);
        if (options.length === 0) {
          setSearchMessage("Hastalık bulunamadı");
        } else {
          setSearchMessage("");
        }
      } catch (error) {
        console.error("Error searching diseases:", error);
        setSearchMessage("Hastalık arama işlemi sırasında bir hata oluştu.");
      }
    } else {
      setDiseaseOptions([]);
      setSearchMessage("");
    }
  };

  const handleAllergyChange = (e) => {
    const { value, checked } = e.target;
    const selectedAllergy = allergyOptions.find(allergy => allergy.id === parseInt(value));
    if (checked) {
      setSelectedAllergies([...selectedAllergies, selectedAllergy]);
    } else {
      setSelectedAllergies(selectedAllergies.filter(allergy => allergy.id !== parseInt(value)));
    }
  };

  const handleDiseaseChange = (e) => {
    const { value, checked } = e.target;
    const selectedDisease = diseaseOptions.find(disease => disease.id === parseInt(value));
    if (checked) {
      setSelectedDiseases([...selectedDiseases, selectedDisease]);
    } else {
      setSelectedDiseases(selectedDiseases.filter(disease => disease.id !== parseInt(value)));
    }
  };

  const handleSaveAllergies = async () => {
    try {
      const allergyIds = selectedAllergies.map(a => a.id);
      const payload = { UserId: id, AllergyIds: allergyIds };
      console.log("Gönderilen veri:", payload);
      await saveUserAllergies(id, allergyIds);
      toastr.success("Alerjiler başarıyla kaydedildi.");
      setSelectedAllergies([]);
      fetchUserProfile();
    } catch (error) {
      console.error("Error saving user allergies:", error);
      toastr.error("Alerjiler kaydedilirken bir hata oluştu.");
    }
  };

  const handleSaveDiseases = async () => {
    try {
      const diseaseIds = selectedDiseases.map(d => d.id);
      const payload = { UserId: id, DiseaseIds: diseaseIds };
      console.log("Gönderilen veri:", payload);
      await saveUserDiseases(id, diseaseIds);
      toastr.success("Hastalıklar başarıyla kaydedildi.");
      setSelectedDiseases([]);
      fetchUserProfile();
    } catch (error) {
      console.error("Error saving user diseases:", error);
      toastr.error("Hastalıklar kaydedilirken bir hata oluştu.");
    }
  };

  return (
    <div className="profile-container">
  
    <div className="user-info mb-4 d-flex align-items-start">
      <img src={profileImage} alt="Profil Resmi" className="profile-image mr-4" />
      <div className="user-details">
        <h2>Kullanıcı Bilgileri</h2>
        {isEditing ? (
          <div className="form-group">
            <label htmlFor="username">Ad:</label>
            <input
              type="text"
              className="form-control"
              id="username"
              name="username"
              value={editInfo.username || ""}
              onChange={handleInputChange}
            />
            <label htmlFor="email" className="mt-2">Email:</label>
            <input
              type="email"
              className="form-control"
              id="email"
              name="email"
              value={editInfo.email || ""}
              onChange={handleInputChange}
            />
            <button className="btn btn-primary mt-3" onClick={handleSave}>Kaydet</button>
            <button className="btn btn-secondary mt-3" onClick={handleEditToggle}>İptal</button>
          </div>
        ) : (
          <div>
            <p>Ad: {userInfo.username}</p>
            <p>Email: {userInfo.email}</p>
            <button className="btn btn-primary" onClick={handleEditToggle}>Düzenle</button>
          </div>
        )}
      </div>
    </div>

    <div className="user-diseases-section d-flex align-items-start">
        <div className="user-diseases">
          <h2>Hastalıklar</h2>
          <div className="search-bar">
            <input
              type="text"
              className="form-control"
              placeholder="Hastalık arayın"
              onChange={handleDiseaseSearch}
            />
            <FaSearch className="search-icon" />
          </div>
          {searchMessage && <p className="error-message">{searchMessage}</p>}
          <div className="disease-options mt-2">
            {diseaseOptions.length > 0 && diseaseOptions.map((disease) => (
              <label key={disease.id} className="disease-option">
                <input
                  type="checkbox"
                  className="mr-2"
                  value={disease.id}
                  onChange={handleDiseaseChange}
                  checked={selectedDiseases.some(d => d.id === disease.id)}
                />
                {disease.name}
              </label>
            ))}
          </div>
          <button className="btn btn-success mt-3" onClick={handleSaveDiseases}>Hastalıkları Kaydet</button>
          <div className="selected-diseases mt-3">
            <h3>Seçilen Hastalıklar:</h3>
            <ul>
              {selectedDiseases.map((disease) => (
                <li key={disease.id} className="list-group-item">{disease.name}</li>
              ))}
            </ul>
          </div>
        </div>
        <div className="disease-image ml-4">
          <img src={profile1} alt="Disease" className="disease-image" />
        </div>
      </div>

      <div className="user-allergies-section d-flex align-items-start mt-4">
        <div className="user-allergies">
          <h2>Alerjiler</h2>
          <div className="search-bar">
            <input
              type="text"
              className="form-control"
              placeholder="Alerji arayın"
              onChange={handleAllergySearch}
            />
            <FaSearch className="search-icon" />
          </div>
          {searchMessage && <p className="error-message">{searchMessage}</p>}
          <div className="allergy-options mt-2">
            {allergyOptions.length > 0 && allergyOptions.map((allergy) => (
              <label key={allergy.id} className="allergy-option">
                <input
                  type="checkbox"
                  className="mr-2"
                  value={allergy.id}
                  onChange={handleAllergyChange}
                  checked={selectedAllergies.some(a => a.id === allergy.id)}
                />
                {allergy.name}
              </label>
            ))}
          </div>
          <button className="btn btn-success mt-3" onClick={handleSaveAllergies}>Alerjileri Kaydet</button>
          <div className="selected-allergies mt-3">
            <h3>Seçilen Alerjiler:</h3>
            <ul>
              {selectedAllergies.map((allergy) => (
                <li key={allergy.id} className="list-group-item">{allergy.name}</li>
              ))}
            </ul>
          </div>
        </div>
        <div className="allergy-image ml-4">
          <img src={profile2} alt="Allergy" className="allergy-image" />
        </div>
      </div>
    </div>
  );
};
