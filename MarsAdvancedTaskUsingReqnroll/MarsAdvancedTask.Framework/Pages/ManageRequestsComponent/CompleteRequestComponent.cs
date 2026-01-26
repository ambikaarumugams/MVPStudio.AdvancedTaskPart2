using MarsAdvancedTask.Framework.Helpers;
using OpenQA.Selenium;

namespace MarsAdvancedTask.Framework.Pages
{
    public class CompleteRequestComponent
    {
        private readonly TestState _state;
        public CompleteRequestComponent(TestState state)
        {
            _state = state;
        }

        //Locators
        private readonly By _completeButtonElement = By.XPath("//button[normalize-space()='Complete']");
        private readonly By _ratingElements = By.XPath("//body[1]/div[1]/div[1]/div[1]/div[2]/div[1]/table[1]/tbody[1]/tr[2]/td[3]/div[1]//i");
        private readonly By _communicationElement = By.XPath("//div[@id='communicationRating']//i");
        private readonly By _serviceElement = By.XPath("//div[@id='serviceRating']//i");
        private readonly By _recommendElement = By.XPath("//div[@id='recommendRating']//i");
        private readonly By _reviewTextBoxElement = By.XPath("//textarea[@id='reviewCommentInput']");

        //Methods
        public void ClickCompleteButton()
        {
            _state.Driver.FindElement(_completeButtonElement).Click();
        }

        public void ClickRating()
        {
            var ratingElements = _state.Driver.FindElements(_ratingElements);
            foreach (var ratingElement in ratingElements)
            {
                ratingElement.Click();
            }
        }

        public void ClickCommunicationRating()
        {
            var communicationStars=_state.Driver.FindElements(_communicationElement);
            foreach(var star in communicationStars)
            {
                star.Click();
            }
        }

        public void ClickServiceRating()
        {
            var serviceStars = _state.Driver.FindElements(_serviceElement);
            foreach(var star in serviceStars)
            {
                star.Click();
            }
        }

        public void ClickRecommendRating()
        {
            var recommendStars = _state.Driver.FindElements(_recommendElement);
            foreach(var star in recommendStars)
            {
                star.Click();
            }
        }

        public void EnterReviewText(string review)
        {
            var reviewText = _state.Driver.FindElement(_reviewTextBoxElement);
            reviewText.SendKeys(review);
        }
    }
}
