import PropTypes from 'prop-types';

const RecipeDisplay = ({ recipe }) => (
  <div className="recipe-display mt-4">
    <h3>Yemek Tarifi</h3>
    <p>{recipe}</p>
  </div>
);

RecipeDisplay.propTypes = {
  recipe: PropTypes.string.isRequired,
};

export default RecipeDisplay;
