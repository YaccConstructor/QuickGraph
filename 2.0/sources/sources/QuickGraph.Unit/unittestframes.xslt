<?xml version="1.0" encoding="UTF-8" ?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:include href ="common.xslt"/>
    <xsl:template match="/TestBatch">
		<html>
			<xsl:comment> saved from url=(0027)http://blog.dotnetwiki.org/ </xsl:comment>
			<head>
                <title>
                    <xsl:value-of select="@EntryAssemblyLocation"/> Test Report
                </title>
                <xsl:call-template name="meta-html" />
            </head>
            <body>
  <frameset cols="20%,*">
	<frame name="left" src="fixture_summary.html" scrolling="yes"/>
    <frame name="middle" src="fixture_details.html" scrolling="yes"/>
  </frameset>
  <noframes>
This document is designed to be viewed using the frames feature.
If you see this message, you are using a
non-frame-capable web client.
</noframes>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>