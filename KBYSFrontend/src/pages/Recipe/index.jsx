import { useState, useContext, useEffect } from 'react';
import axios from 'axios';
import RecipeForm from '../../components/RecipeForm';
import RecipeDisplay from '../../components/RecipeDisplay';
import { LoginContext } from "../../state/context";
import { useNavigate } from "react-router-dom";
import { toast, ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Recipe.css';
import background from "../../assets/yapazeka.png"; // Arkaplan resminiz

export function Recipe() {
  const [recipe, setRecipe] = useState('');
  const [error, setError] = useState(null);
  const { id } = useContext(LoginContext);
  const navigate = useNavigate();

  useEffect(() => {
    if (id === "0") {
      toast.error("Lütfen önce giriş yapın", {
        onClose: () => navigate('/login')
      });
    }
  }, [id, navigate]);

  const fetchRecipe = async (data) => {
    const prompt = `Malzemeler: ${data.ingredients}\nÖğün Türü: ${data.mealType}\nYapılış Süresi: ${data.cookingTime}\nBu bilgilere göre bir yemek tarifi oluştur.`;

    const options = {
      method: 'POST',
      url: 'https://chat-gpt26.p.rapidapi.com/',
      headers: {
        'x-rapidapi-key': '11bfeac787msh5ef7699bc886e13p1c43ddjsn765c688fedb5',
        'x-rapidapi-host': 'chat-gpt26.p.rapidapi.com',
        'Content-Type': 'application/json'
      },
      data: {
        model: 'gpt-3.5-turbo',
        messages: [
          {
            role: 'user',
            content: prompt
          }
        ]
      }
    };

    try {
      const response = await axios.request(options);
      setRecipe(response.data.choices[0].message.content.trim());
      setError(null); 
    } catch (error) {
      console.error('Error fetching recipe:', error);
      setError(error.message);
    }
  };

  if (id === "0") {
    return (
      <>
        <ToastContainer />
      </>
    );
  }

  return (
    <div className="recipe-page" style={{ backgroundImage: `url(${background})` }}>
      <div className="form-container">
        <h1 className="text-center mb-4">Yemek Tarifi Üretici</h1>
        <div className="form-content">
          <RecipeForm onSubmit={fetchRecipe} />
          {error && <p className="error-message text-danger text-center mt-3">Hata: {error}</p>}
          {recipe && <RecipeDisplay recipe={recipe} />}
        </div>
      </div>
      <ToastContainer />
    </div>
  );
}
