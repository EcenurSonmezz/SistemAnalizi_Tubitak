import axios from 'axios';

// Kullanıcı profili verisini çekme
export const getUserProfile = async (userId) => {
  const response = await axios.get(`/api/user/${userId}`);
  console.log("UserProfile", response.data);
  return response.data;
};

// Alerji seçeneklerini çekme
export const getAllergyOptions = async (query = '') => {
  const response = await axios.get(`/api/Allergies/Search/search?query=${query}`);
  console.log("getAllergyOptions", response.data);
  return response.data.$values || [];
};

// Kullanıcı bilgilerini güncelleme
export const updateUserProfile = async (userId, editInfo) => {
  const response = await axios.put(`/api/user/${userId}`, editInfo);
  return response.data;
};

// Kullanıcı alerjilerini kaydetme
export const saveUserAllergies = async (userId, allergyIds) => {
  const payload = { UserId: userId, AllergyIds: allergyIds };
  const response = await axios.post(`/api/Userallergies/Add`, payload);
  return response.data;
};

// Hastalık seçeneklerini çekme
export const getDiseaseOptions = async (query = '') => {
  const response = await axios.get(`/api/Diseases/Search?query=${query}`);
  console.log("getDiseaseOptions", response.data);
  return response.data.$values || [];
};

// Kullanıcı hastalıklarını kaydetme
export const saveUserDiseases = async (userId, diseaseIds) => {
  const payload = { UserId: userId, DiseaseIds: diseaseIds };
  const response = await axios.post(`/api/User/AddUserDiseases`, payload);
  return response.data;
};
