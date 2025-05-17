document.addEventListener('DOMContentLoaded', () => {
    const questionInput = document.getElementById('questionInput');
    const askButton = document.getElementById('askButton');
    const answerOutput = document.getElementById('answerOutput');

    askButton.addEventListener('click', async () => {
        const question = questionInput.value.trim();

        if (question) {
            answerOutput.textContent = '查詢中...';

            try {
                const response = await fetch('http://localhost:5097/api/fqa/query', {
                    method: 'POST',
                    headers: {
                        'Content-Type': 'application/json'
                    },
                    body: JSON.stringify({ userInput: question })
                });

                if (response.ok) {
                    const data = await response.json();
                    answerOutput.textContent = data.answer;
                } else {
                    answerOutput.textContent = `查詢失敗，錯誤代碼：${response.status}`;
                }
            } catch (error) {
                answerOutput.textContent = `查詢過程中發生錯誤：${error.message}`;
            } finally {
                questionInput.value = ''; // 清空輸入框
            }
        } else {
            answerOutput.textContent = '請輸入您的問題。';
        }
    });

    questionInput.addEventListener('keypress', (event) => {
        if (event.key === 'Enter') {
            askButton.click();
        }
    });
});