# [CHANGELOG] Webex Teams API Client for .NET

#### ほかの言語のCHANGELOG
* [English CHANGELOG is here](https://github.com/thrzn41/WebexTeamsAPIClient/blob/master/CHANGELOG.md) ([英語のCHANGELOGはこちら](https://github.com/thrzn41/WebexTeamsAPIClient/blob/master/CHANGELOG.md))


## Webex Teams API Client Changelog

> NOTE: 日付形式は、`yyyy-MM-dd`です。

---
### [2020-02-07] Version 1.7.1

#### [NEW FEATURES]

* AdaptiveCard機能(カードの作成やユーザからの入力受け取り)。
* SpaceMeetingInfo機能。

---
### [2019-02-15] Version 1.6.2

#### [FIXED DEFECTS]
* ファイルサイズが大きい時に、`CopyFileDataToStreamAsync()`が`OutOfMemoryException`を送出する。
* `personId == null`の時、`DetectPersonIdType()`が例外を送出する。

#### [OBSOLETES]
* `TeamsListResult.GetListResultEnumerator(TeamsRetry, TeamsListResultFunc)`を非推奨メソッドにしました。TeamsRetryHandlerかTeamsRetryOnErrorHandlerを利用してください。

---
### [2018-12-17] Version 1.6.1

#### [NEW FEATURES]

* HTTP 429や500, 502, 503, 504のときにリトライするためのTeamsRetryHandler/TeamsRetryOnErrorHandler機能
* ファイルデータをStreamにコピーする機能。
* TeamsResultInfoのための、TransactionId/ListTransactionId。
* TeamsResultInfoのための、TeamsResourceOperation。
* TeamsResultInfoのための、RequestLine。

#### [OBSOLETES]
* `RetryExecutor`を非推奨クラスにしました。TeamsRetryHandlerかTeamsRetryOnErrorHandlerを利用してください。

---
### [2018-10-12] Version 1.5.1

#### [NEW FEATURES]

* Guest Issuer機能。
* GroupResource/GroupResourceMembership機能。

---
### [2018-08-06] Version 1.4.1

#### [NEW FEATURES]

* Webhookの更新と再Activate機能。
* `MarkdownBuilder`での、Group Mention(All)機能。

#### [OBSOLETES]

* メソッド名に誤記があるため、以下のメソッドを非推奨にしました。  
修正済みバージョンをご利用ください。非推奨のメソッドも継続して動作します。
  * `MarkdownBuilder.AppendOrderdList()`
  * `MarkdownBuilder.AppendOrderdListFormat()`
  * `MarkdownBuilder.AppendUnorderdList()`
  * `MarkdownBuilder.AppendUnorderdListFormat()`

---
### [2018-06-13] Version 1.3.1

#### [NEW FEATURES]

* Pagination機能を簡単にするための、`TeamsListResultEnumerator`。

---
### [2018-06-04] Version 1.2.2

#### [NEW FEATURES]
最初のリリースです。
このプロジェクトは、`Thrzn41.CiscoSpark`から移動されました。  
`Thrzn41.CiscoSpark`のすべての機能が継承されています。

* Webex Teamsの基本的なAPI(List/Get/Create Message, Spaceなど)。
* Webex TeamsのAdmin API(List/Get Event, Licenseなど)。
* ストレージに保存するTokenの暗号化と復号。
* List API用のPagination機能。Paginationを簡単にするためのEnumerator。
* Retry-after値の処理とRetry executor。
* Markdown builder。
* エラーコードや詳細の取得。
* Webhook secretの検証とWebhook notification manager、Webhook event handler。
* OAuth2 helper
* 簡易Webhookサーバ機能。

---
