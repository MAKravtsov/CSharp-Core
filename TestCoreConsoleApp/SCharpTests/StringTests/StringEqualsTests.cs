using NUnit.Framework;

namespace SCharpTests.StringTests
{
    public class StringEqualsTests
    {
        /*
         * 1) Создать неинтернированную строку не так уж и легко.
            Пока найден только такой путь:
            var suffix = "3";
            var first = "12" + suffix;
            var second = "12" + suffix;
         * 2) При сравнении строк сначала пытаемся сравнить ссылки (если строки интеринированы, то дальнейших проверок нет),
         *  если не получается, то сравниваем по значению.
         * 3) Когда интернируем строки через метод Intern, сама строка все также не является интернированной,
         *    она все также лежит где-то в рандомном месте кучи.
         *  А вот метод Intern уже возвращает другую ссылку, из таблицы интернированных строк.
         *  Аналогичная ситуация и с IsInterned.
         *  Он ищет в таблице интернированных строк по хэшу значения и если находит,
         *    то возвращает ссылку на строку из таблицы интернированных строк
         */
        
        /// <summary>
        /// В большинстве случаев .net сам понимает, что нужно интернировать строки.
        /// В этих ситуациях сразу возвращается ссылку на строку в таблице интернированных строк.
        /// Сравнение происходит по ссылке.
        /// </summary>
        [Test]
        public void StringEquals_InternedStrings_ReferenceEquals()
        {
            // Arrange
            var first = "123";
            var second = "123";

            // Act
            var equalsResult = first.Equals(second);
            var refEqualsResult = ReferenceEquals(first, second);
            var firstIsInterned = string.IsInterned(first);
            var secondIsInterned = string.IsInterned(second);
            
            // Assert
            Assert.True(equalsResult);
            Assert.True(refEqualsResult);
            Assert.NotNull(firstIsInterned);
            Assert.NotNull(secondIsInterned);
        }

        /// <summary>
        /// Если строки неинтернированы, то сравнение происходит по значению.
        /// Оператор == вернет true, но под капотом сравнение ссылок покажет false
        /// </summary>
        [Test]
        public void StringEquals_UnInternedStrings_EqualsButNotReferenceEquals()
        {
            // Arrange
            var suffix = "3";
            var first = "12" + suffix;
            var second = "12" + suffix;
            
            // Act
            var equalsResult = first.Equals(second);
            var refEqualsResult = ReferenceEquals(first, second);
            var firstIsInterned = string.IsInterned(first);
            var secondIsInterned = string.IsInterned(second);
            
            // Assert
            Assert.True(equalsResult);
            Assert.False(refEqualsResult);
            Assert.Null(firstIsInterned);
            Assert.Null(secondIsInterned);
        }
        
        /// <summary>
        /// Строки можно принудительно заинтеринровать с помощью метода Intern.
        /// При этом изначальные строки останутся на своем месте и ссылки будут разные.
        /// А вот строки, которые возврващаются из методов Intern или IsInterned будут
        ///   ссылаться на одно и тоже место в таблице интернированных строк.
        /// </summary>
        [Test]
        public void StringEquals_InternUnInternedStrings_OnlyInternedStringsReferenceEquals()
        {
            // Arrange
            var suffix = "3";
            var first = "12" + suffix;
            var second = "12" + suffix;
            
            // интернируем 1ую строку
            var internedFirst = string.Intern(first);
            
            // если б не заинтернировали 1ую строку, то было бы null.
            // Но в данном случае в пуле интернированных строк уже строка равная 2ой, поэтому мы можем ее вернуть.
            // В данном случае можно было б написать вот так, ничего б не изменилось: string.Intern(second);
            var internedSecond = string.IsInterned(second); 
            
            // Act
            var equalsResult = first.Equals(second);
            var refEqualsResult = ReferenceEquals(first, second);
            var internedRefEqualsResult = ReferenceEquals(internedFirst, internedSecond);
            var firstIsInterned = string.IsInterned(first);
            var secondIsInterned = string.IsInterned(second);
            
            // Assert
            Assert.True(equalsResult);
            Assert.False(refEqualsResult);
            Assert.True(internedRefEqualsResult);
            Assert.NotNull(firstIsInterned);
            Assert.NotNull(secondIsInterned);
        }
    }
}